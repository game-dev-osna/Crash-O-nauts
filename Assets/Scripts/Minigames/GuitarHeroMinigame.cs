using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuitarHeroMinigame : Minigame
{
    //Sounds
    private AudioClip correctSound;
    private AudioClip successSound;
    private AudioClip failSound;

    // Changed by difficulty
    public float duration = 3;
    public int projectileCount = 3;
    public float speedFactor = 0.01f;
    public float hitboxOffset = 30.0f;
    public float hitboxSize = 50.0f;

    private float onPressScaleFactor = 1.2f;

    private RectTransform buttonPromptRectTransform;
    public Action<UnityEngine.InputSystem.InputAction.CallbackContext> triggerButtonPressedCallback;
    public Action<UnityEngine.InputSystem.InputAction.CallbackContext> triggerButtonReleasedCallback;

    private Queue<float> spawntimes = new Queue<float>();
    private List<RectTransform> activeProjectiles = new List<RectTransform>();
    private float startTime;

    private Sprite squareSprite;
    private int hitProjectiles;

    public void OnTriggerButtonPressed()
    {
        buttonPromptRectTransform.localScale = Vector3.one * onPressScaleFactor;

        // Check if projectile is in target
        if (activeProjectiles.Count > 0
        && activeProjectiles[0].anchoredPosition.x >= hitboxOffset
        && activeProjectiles[0].anchoredPosition.x <= hitboxSize + hitboxOffset)
        {
            RectTransform hitProjectile = activeProjectiles[0];
            activeProjectiles.Remove(hitProjectile);
            UnityEngine.Object.Destroy(hitProjectile.gameObject);

            hitProjectiles++;
            player.audioSource.PlayOneShot(correctSound);
            Debug.Log("Hit projectile");
        }
        else
        {
            Debug.Log("Missed projectile");

            OnMinigameFail.Invoke();
        }
    }

    public void OnTriggerButtonReleased()
    {
        buttonPromptRectTransform.localScale = Vector3.one;
    }

    protected override void OneTimeSetup()
    {
        squareSprite = Resources.Load<Sprite>("Images/Square");
        correctSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Correct");
        successSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Success");
        failSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Fail");
    }

    protected override void SetupMinigame()
    {
        spawntimes.Clear();
        activeProjectiles.Clear();

        // Load button prompt
        Sprite buttonPromptSprite = Resources.Load<Sprite>("Images/Buttons/" + player.controlDevice.ToString() + "/Interact");

        hitProjectiles = 0;
        startTime = Time.time;

        // Change controls to fit the minigame
        player.controls.Player.Disable();
        player.controls.GuitarHeroMinigame.Enable();
        triggerButtonPressedCallback = delegate { OnTriggerButtonPressed(); };
        triggerButtonReleasedCallback = delegate { OnTriggerButtonReleased(); };
        player.controls.GuitarHeroMinigame.Trigger.started += triggerButtonPressedCallback;
        player.controls.GuitarHeroMinigame.Trigger.canceled += triggerButtonReleasedCallback;

        // Spawn triggerzone
        GameObject triggerZoneGameObject = new GameObject("Safezone");
        RectTransform triggerZoneRectTransform = triggerZoneGameObject.AddComponent<RectTransform>();
        Image spriteImage = triggerZoneGameObject.AddComponent<Image>();
        spriteImage.sprite = squareSprite;
        spriteImage.color = Color.green;
        triggerZoneRectTransform.sizeDelta = new Vector2(hitboxSize, minigameScreen.sizeDelta.y);
        triggerZoneRectTransform.anchorMax = Vector2.zero;
        triggerZoneRectTransform.anchorMin = Vector2.zero;
        triggerZoneRectTransform.SetParent(minigameScreen);
        triggerZoneRectTransform.localScale = Vector3.one;
        triggerZoneRectTransform.localRotation = Quaternion.identity;
        triggerZoneRectTransform.localPosition = Vector3.zero;
        triggerZoneRectTransform.anchoredPosition = new Vector2(triggerZoneRectTransform.sizeDelta.x / 2 + hitboxOffset, minigameScreen.sizeDelta.y * 0.5f);

        // Spawn button prompt
        GameObject buttonPromptGameObject = new GameObject("ButtonPrompt");
        buttonPromptRectTransform = buttonPromptGameObject.AddComponent<RectTransform>();
        Image buttonPromptImage = buttonPromptGameObject.AddComponent<Image>();
        buttonPromptImage.sprite = buttonPromptSprite;
        buttonPromptRectTransform.sizeDelta = new Vector2(hitboxSize, hitboxSize);
        buttonPromptRectTransform.anchorMax = Vector2.zero;
        buttonPromptRectTransform.anchorMin = Vector2.zero;
        buttonPromptRectTransform.SetParent(minigameScreen);
        buttonPromptRectTransform.localScale = Vector3.one;
        buttonPromptRectTransform.localRotation = Quaternion.identity;
        buttonPromptRectTransform.localPosition = Vector3.zero;
        buttonPromptRectTransform.anchoredPosition = new Vector2(triggerZoneRectTransform.anchoredPosition.x, minigameScreen.sizeDelta.y + buttonPromptRectTransform.sizeDelta.y);

        // Calculate spawn times of the projectiles
        float spawnEach = duration / projectileCount;

        for (int i = 0; i < projectileCount; i++)
            spawntimes.Enqueue(i * spawnEach);
    }

    protected override void TearDown()
    {
        player.controls.GuitarHeroMinigame.Disable();
        player.controls.Player.Enable();
        player.controls.GuitarHeroMinigame.Trigger.started -= triggerButtonPressedCallback;
        player.controls.GuitarHeroMinigame.Trigger.canceled -= triggerButtonReleasedCallback;
    }

    protected override void Update()
    {
        // Check if a new projectile should be spawned
        float currentTime = Time.time - startTime;
        if (spawntimes.Count > 0 && currentTime >= spawntimes.Peek())
        {
            Debug.Log("Spawn new projectile");

            spawntimes.Dequeue();
            GameObject projectileGameObject = new GameObject("Projectile" + activeProjectiles.Count);
            RectTransform projectileRectTransform = projectileGameObject.AddComponent<RectTransform>();
            Image spriteImage = projectileGameObject.AddComponent<Image>();
            spriteImage.sprite = squareSprite;
            spriteImage.color = Color.red;
            projectileRectTransform.sizeDelta = new Vector2(10, minigameScreen.sizeDelta.y);
            projectileRectTransform.anchorMax = Vector2.zero;
            projectileRectTransform.anchorMin = Vector2.zero;
            projectileRectTransform.SetParent(minigameScreen);
            projectileRectTransform.localPosition = Vector3.zero;
            projectileRectTransform.localScale = Vector3.one;
            projectileRectTransform.localRotation = Quaternion.identity;
            projectileRectTransform.anchoredPosition = new Vector2(minigameScreen.sizeDelta.x - projectileRectTransform.sizeDelta.x / 2, minigameScreen.sizeDelta.y * 0.5f);
            activeProjectiles.Add(projectileRectTransform);
        }

        List<RectTransform> projectilesToDestroy = new List<RectTransform>();
        foreach (RectTransform projectile in activeProjectiles)
        {
            projectile.Translate(-1 * speedFactor, 0, 0, Space.Self);

            // Projectile is out of sight and can be destroyed
            if(projectile.anchoredPosition.x < projectile.sizeDelta.x / 2.0f)
            {
                player.audioSource.PlayOneShot(failSound);
                OnMinigameFail.Invoke();
                return;
            }
        }

        // Lost because all projectiles got destroyed
        if(activeProjectiles.Count == 0 
        && spawntimes.Count == 0)
        {

            Debug.Log("Hit: " + hitProjectiles + " Count: " + projectileCount);
            if(projectileCount == hitProjectiles)
            {
                Debug.Log("Won");
                player.audioSource.PlayOneShot(successSound);
                OnMinigameSuccess.Invoke();
            }
            else
            {
                Debug.Log("Failed");
                player.audioSource.PlayOneShot(failSound);
                OnMinigameFail.Invoke();
            }
        }
    }
}
