using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    public float gravityStrength = 9.7f;
    //[SerializeField]
    public float maxTime;

    //private float life;

    GameManager manager;
    List<Player> attractedPlayers;
    private ParticleSystem explosion;
    private AudioSource explosionSound;

    public Color coreColor;
    public Color lavaColor;
    public Color dirtColor;
    public Color grassColor;
    public Color stoneColor;

    public float coreRange;
    public float lavaRange;
    public float dirtRange;
    public float grassRange;
    public float stoneRange;

    public float fadeRange;
    public Vector3 crashPoint;
    public Vector3 crashNormal;
    public Vector3 crashDirection;

    public float speed = 1.0f;

    [Range(0.0f, 1.0f)]
    public float apocalypse;
    const float apocalypseMax = 0.5f;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        attractedPlayers = new List<Player>();
        explosion = GetComponentInChildren<ParticleSystem>();
        explosionSound = GetComponentInChildren<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float targetScale = 1.0f;
        if (manager.activePlanet != this)
            targetScale = 0.2f;
        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }

    public void KillAllPlayersOnSurface()
    {
        foreach (Player player in attractedPlayers)
            player.Kill();
    }

    public void SetApocalypseLevel(float level)
    {
        apocalypse = level * apocalypseMax;
        if (!explosion.isPlaying && level > 0.9f)
        {
            explosion.Play();
            explosionSound.Play();
            //TODO mehrere Explosionen abspielen
        }
    }

    // Update is called once per frame
    void Update()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        mat.SetColor("_ColorCore", coreColor);
        mat.SetColor("_ColorLava", lavaColor);
        mat.SetColor("_ColorDirt", dirtColor);
        mat.SetColor("_ColorGrass", grassColor);
        mat.SetColor("_ColorStone", stoneColor);

        mat.SetFloat("_RangeCore", coreRange);
        mat.SetFloat("_RangeLava", lavaRange);
        mat.SetFloat("_RangeDirt", dirtRange);
        mat.SetFloat("_RangeGrass", grassRange);
        mat.SetFloat("_RangeStone", stoneRange);

        mat.SetFloat("_FadeRange", fadeRange);
        mat.SetFloat("_Apocalypse", apocalypse);


        float targetScale = 1.0f;
        if (manager.activePlanet != this)
            targetScale = 0.2f;

        targetScale = Mathf.Lerp(transform.localScale.x, targetScale, Time.deltaTime * speed);
        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }

    public void Init(float gravity)//, float lifeTime)
    {
        this.gravityStrength = gravity;
        //this.lifeTime = lifeTime;
        //life = lifeTime;
    }

    public void AddPlayer(Player player)
    {
        attractedPlayers.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        attractedPlayers.Remove(player);
    }

    private void FixedUpdate()
    {
        foreach (Player player in attractedPlayers)
        {
            Vector3 gravityDirection = (player.transform.position - transform.position).normalized;
            player.rb.AddForce(gravityDirection * gravityStrength, ForceMode.Acceleration);
        }
    }
}
