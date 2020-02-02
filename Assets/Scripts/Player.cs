using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        public float accelerationSpeed;
        public float turnSpeed;
        public float maxWalkSpeed;
        public float runningMultiplier;
    }

    public enum ControlDevice
    {
        Keyboard,
        Xbox,
        Ps4
    }

    private ControlDevice _device;

    public ControlDevice controlDevice
    {
        get
        {
            return _device;
        }
        set
        {
            Debug.Log("I am " + value);

            _device = value;

            interactionButtonPromptImage.sprite = Resources.Load<Sprite>("Images/Buttons/" + _device.ToString() + "/Interact");
        }
    }

    private int id;

    private Animator anim;

    public AudioSource audioSource;

    [SerializeField]
    private AudioSource walkingSound;

    [SerializeField]
    public Stats stats;
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private ParticleSystem jetpack;
    [SerializeField]
    private AudioClip jetpackSound;

    // Minigame specifics
    public RectTransform minigameScreen;
    public Image interactionButtonPromptImage;

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public InputUser user;
    [HideInInspector]
    public Planet planet;
    public Controls controls;

    private GameManager manager;
    private bool running = false;
    private Vector3 moveInput;
    private float walkSpeed;
    private bool isAirborne = false;
    int airCounter = 0;

    //pickups
    private PickUp hoveredPickUp;
    private PickUp heldPickup;
    private GameObject shuttle;
    private Dictionary<Type, float> minigameBooster = new Dictionary<Type, float>();

    [SerializeField]
    public PlayerState playerState = PlayerState.Dead;

    private RandomSkin m_RandomSkin;

    public enum PlayerState
    {
        Normal,
        OverPickup,
        OverShuttle,
        Travelling,
        Dead,
        InMinigame
    }

    public bool isAlive()
    {
        return playerState != PlayerState.Dead;
    }

    public bool IsOverShuttle()
    {
        return playerState == PlayerState.OverShuttle;
    }

    #region Init functions
    private void Awake()
    {
        controls = new Controls();

        manager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();

        shuttle = GameObject.Find("Shuttle");
    }

    public void Init(InputUser user, int id)
    {
        this.user = user;
        this.id = id;
        

        anim = GetComponentInChildren<Animator>();
        m_RandomSkin = GetComponentInChildren<RandomSkin>();

        DontDestroyOnLoad(this);

    }

    public void LoadSelection()
    {
        controls.Player.Run.started += _ => running = true;
        controls.Player.Run.canceled += _ => running = false;
        controls.Player.Move.started += context => OnMove(context.ReadValue<Vector2>());
        controls.Player.Move.performed += context => OnMove(context.ReadValue<Vector2>());
        controls.Player.Move.canceled += context => OnMove(context.ReadValue<Vector2>());
        controls.Player.Interact.started += delegate { OnInteractButtonPressed(); };
        controls.Player.Jump.started += delegate { OnJumpPressed(); };
        controls.Player.Start.started += delegate { OnStartPressed(); };

        user.AssociateActionsWithUser(controls);

        GetComponentInChildren<RandomColor>().SetColor(PlayerSkinManager.Instance.Colors[id]);
        m_RandomSkin.ToggleSkin(PlayerSkinManager.Instance.GetSelection(id));
    }

    #endregion

    private void OnJumpPressed()
    {
        if (!isAirborne)
        {
            anim.SetBool("isJumping", true);
            jetpack.Play();
            audioSource.PlayOneShot(jetpackSound);
            isAirborne = true;
            rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);
        }
    }

    private void OnStartPressed()
    {
        if (PauseMenu.Instance)
            PauseMenu.Instance.TogglePause();
    }

    private void OnInteractButtonPressed()
    {
        //if (!isAlive() || playerState == PlayerState.Travelling)
            //return;
        Debug.Log("Pressed interaction button");

        if (playerState == PlayerState.OverPickup)
        {
            anim.SetBool("isInteracting", true);

            // Check if the player can take a new pickup
            if (heldPickup != null)
                return;

            // Another player is trying this minigame
            if (hoveredPickUp.minigame.isActive)
                return;

            // Lock the player in place for the minigame
            rb.isKinematic = true;

            interactionButtonPromptImage.gameObject.SetActive(false);

            playerState = PlayerState.InMinigame;

            // Start the pickup minigame for this player
            hoveredPickUp.minigame.InitMinigame(this);
            hoveredPickUp.minigame.OnMinigameSuccess.AddListener(delegate { OnPlayerFinishedMinigame(hoveredPickUp); });
            hoveredPickUp.minigame.OnMinigameFail.AddListener(OnPlayerFailedMinigame);
        }
        else if (playerState == PlayerState.OverShuttle)
        {
            // Check if the player has a pickup to give to the shuttle
            if (heldPickup != null)
            {
                interactionButtonPromptImage.gameObject.SetActive(false);
                manager.ReturnPickup(heldPickup);
                heldPickup = null;
            }
        }
    }

    private void OnPlayerFinishedMinigame(PickUp pickUpToCollect)
    {
        // Give physics back to the player
        rb.isKinematic = false;

        playerState = PlayerState.Normal;

        // Attach the pickup to player
        Debug.Log(pickUpToCollect);
        Debug.Log(pickUpToCollect.minigame);
        pickUpToCollect.minigame.EndMinigame();
        heldPickup = pickUpToCollect;
        heldPickup.transform.parent.SetParent(transform);
        heldPickup.transform.parent.Translate(manager.playersDistance, 0, 0, Space.World);
        heldPickup.transform.parent.position += transform.up * 5f;
        heldPickup.GetComponent<Collider>().enabled = false;

        foreach (Player player in manager.players)
            player.UnregisterHoveredPickUp(heldPickup);

        hoveredPickUp = null;

        anim.SetBool("isInteracting", false);
    }

    public void UnregisterHoveredPickUp(PickUp pickUp)
    {
        if(hoveredPickUp == pickUp)
        {
            hoveredPickUp = null;
            if(playerState != PlayerState.Dead)
                playerState = PlayerState.Normal;

            interactionButtonPromptImage.gameObject.SetActive(false);
        }
    }

    private void OnPlayerFailedMinigame()
    {
        // Give physics back to the player
        rb.isKinematic = false;

        playerState = PlayerState.Normal;

        hoveredPickUp.minigame.EndMinigame();

        anim.SetBool("isInteracting", false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerState == PlayerState.InMinigame)
            return;

        if (other.gameObject.CompareTag("Pickup"))
        {
            if (heldPickup != null)
                return;

            hoveredPickUp = other.gameObject.GetComponent<PickUp>();
            if(hoveredPickUp.minigame.isActive)
                interactionButtonPromptImage.gameObject.SetActive(false);
            else
                interactionButtonPromptImage.gameObject.SetActive(true);
            playerState = PlayerState.OverPickup;
        }
        else if (other.gameObject.CompareTag("Shuttle"))
        {
            if (playerState == PlayerState.OverPickup
            && heldPickup == null)
                return;

            if(heldPickup)
                interactionButtonPromptImage.gameObject.SetActive(true);

            Debug.Log("Entered shuttle trigger");

            playerState = PlayerState.OverShuttle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerState == PlayerState.InMinigame)
            return;

        Debug.Log("Exited trigger");

        interactionButtonPromptImage.gameObject.SetActive(false);

        hoveredPickUp = null;
        playerState = PlayerState.Normal;
    }

    public void SetPlanet(Planet planet)
    {
        if (this.planet)
            this.planet.RemovePlayer(this);
        this.planet = planet;
        this.planet.AddPlayer(this);

        //transform.parent = planet.transform;
    }

    private void Update()
    {
        model.SetActive(playerState != PlayerState.Dead && playerState != PlayerState.Travelling);
    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.Travelling || playerState == PlayerState.Dead)
            return;

        UpdateMovement(Time.fixedDeltaTime);
        if (isAirborne && (playerState != PlayerState.Dead))
        {
            if (airCounter >=1)
            {
                RaycastHit hit;
                int layerMask = 1 << 10;
                if (Physics.Raycast(transform.position, planet.gameObject.transform.position - transform.position, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.distance <= 0.01f)
                    {
                        isAirborne = false;
                        anim.SetBool("isJumping", false);
                        airCounter = 0;
                    }
                }

            }
            else
            {
                airCounter++;
            }
        }
        else
        {
            isAirborne = false;
        }
    }

    public void Kill()
    {
        playerState = PlayerState.Dead;
        anim.SetBool("isDead", true);
    }

    public void Revive()
    {
        Revive(transform.position);
        anim.SetBool("isDead", false);
    }

    public void Revive(Vector3 pos)
    {
        playerState = PlayerState.Normal;
        transform.position = pos;
    }

    private void UpdateMovement(float deltaTime)
    {
        if (playerState == PlayerState.Dead)
            return;


        float rawSpeedInput = -this.moveInput.x;
        //todo über input system lösen
        float speedInputAbs = Mathf.Abs(rawSpeedInput);
        if (speedInputAbs < 0.1f)
            speedInputAbs = 0.0f;
        else
            speedInputAbs = Mathf.Min((speedInputAbs - 0.1f) * 3.0f, 1.0f);
        int speedInputDir = rawSpeedInput > 0 ? 1 : -1;

        Vector3 planetDirection = transform.position - planet.transform.position;
        Vector3 up = planetDirection.normalized;
        Vector3 right = planet.transform.up;
        Vector3 forward = Vector3.Cross(up, right) * speedInputDir;

        float targetWalkSpeed = speedInputAbs * stats.maxWalkSpeed;

        if (targetWalkSpeed > 0f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (running)
        {
            targetWalkSpeed *= stats.runningMultiplier;

            if (targetWalkSpeed > 0f)
            {
                anim.SetBool("isRunning", true);
                
            }
        }
        else
        {
            anim.SetBool("isRunning", false);

        }

        walkSpeed = Mathf.Lerp(walkSpeed, targetWalkSpeed, deltaTime * stats.accelerationSpeed);

        transform.rotation = Quaternion.LookRotation(forward, up);

        /*Vector3 targetVelocity = forward * walkSpeed;
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = targetVelocity - velocity;*/

        rb.MovePosition(rb.position + forward * walkSpeed * deltaTime);

        /*Vector3 predictedUp = Quaternion.AngleAxis(
            rb.angularVelocity.magnitude * Mathf.Rad2Deg * 0.3f / walkSpeed,
            rb.angularVelocity
        ) * transform.up;
        Vector3 torqueVector = Vector3.Cross(predictedUp, up);
        rb.AddTorque(torqueVector * walkSpeed * walkSpeed);*/

        /*transform.up = up;
        transform.right = right;
        transform.forward = forward;*/
    }

    private void OnMove(Vector2 direction)
    {
        moveInput = direction;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
