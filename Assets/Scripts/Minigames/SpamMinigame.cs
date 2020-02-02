using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SpamMinigame : Minigame
{
    // Sounds
    private AudioClip spamSound;
    private AudioClip successSound;
    private AudioClip failSound;

    // Difficulty settings
    private float startPercentage = 0.5f;
    private float increaseValue = 0.05f;
    private float decreaseValue = 0.05f;

    // Visual settings
    private Color minAmountColor = Color.red;
    private Color maxAmountColor = Color.green;
    private float onPressScaleFactor = 1.2f;

    private Sprite buttonPromptSprite;
    private Sprite squareSprite;
    private Image fillImage;
    private RectTransform buttonPromptRectTransform;
    private Action<InputAction.CallbackContext> spamButtonPressedCallback;
    private Action<InputAction.CallbackContext> spamButtonReleasedCallback;

    private void OnSpamButtonPressed()
    {
        player.audioSource.PlayOneShot(spamSound);

        fillImage.fillAmount += increaseValue;

        fillImage.color = Color.Lerp(minAmountColor, maxAmountColor, fillImage.fillAmount);

        buttonPromptRectTransform.localScale = Vector3.one * onPressScaleFactor;

        // Player filled the image and won the minigame
        if (fillImage.fillAmount >= 1.0f)
        {
            player.audioSource.PlayOneShot(successSound);
            OnMinigameSuccess.Invoke();
        }
            
    }

    private void OnSpamButtonReleased()
    {
        buttonPromptRectTransform.localScale = Vector3.one;
    }

    protected override void OneTimeSetup()
    {
        
        squareSprite = Resources.Load<Sprite>("Images/Square");
        spamSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Spam");
        successSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Success");
        failSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Fail");
    }

    protected override void SetupMinigame()
    {
        // Load button prompt
        buttonPromptSprite = Resources.Load<Sprite>("Images/Buttons/" + player.controlDevice.ToString() + "/Interact");

        // Change controls to fit the minigame
        player.controls.Player.Disable();
        player.controls.SpamMinigame.Enable();
        spamButtonPressedCallback = delegate { OnSpamButtonPressed(); };
        spamButtonReleasedCallback = delegate { OnSpamButtonReleased(); };
        player.controls.SpamMinigame.SPAM.started += spamButtonPressedCallback;
        player.controls.SpamMinigame.SPAM.canceled += spamButtonReleasedCallback;

        GameObject fillImageGameObject = new GameObject("FillImage");
        RectTransform fillImageRectTransform = fillImageGameObject.AddComponent<RectTransform>();
        fillImage = fillImageGameObject.AddComponent<Image>();
        fillImage.sprite = squareSprite;
        fillImage.type = Image.Type.Filled;
        fillImage.fillMethod = Image.FillMethod.Vertical;
        fillImage.fillAmount = startPercentage;
        fillImageRectTransform.sizeDelta = minigameScreen.sizeDelta;
        fillImageRectTransform.anchorMax = Vector2.zero;
        fillImageRectTransform.anchorMin = Vector2.zero;
        fillImageRectTransform.SetParent(minigameScreen);
        fillImageRectTransform.localRotation = Quaternion.identity;
        fillImageRectTransform.localScale = Vector3.one;
        fillImageRectTransform.localPosition = Vector3.zero;
        fillImageRectTransform.anchoredPosition = new Vector2(minigameScreen.sizeDelta.x * 0.5f, minigameScreen.sizeDelta.y * 0.5f);

        GameObject buttonPromptGameObject = new GameObject(buttonPromptSprite.name);
        buttonPromptRectTransform = buttonPromptGameObject.AddComponent<RectTransform>();
        Image buttonPromptImage = buttonPromptGameObject.AddComponent<Image>();
        buttonPromptImage.sprite = buttonPromptSprite;
        buttonPromptRectTransform.sizeDelta = new Vector2(50, 50);
        buttonPromptRectTransform.anchorMax = Vector2.zero;
        buttonPromptRectTransform.anchorMin = Vector2.zero;
        buttonPromptRectTransform.SetParent(minigameScreen);
        buttonPromptRectTransform.localRotation = Quaternion.identity;
        buttonPromptRectTransform.localScale = Vector3.one;
        buttonPromptRectTransform.localPosition = Vector3.zero;
        buttonPromptRectTransform.anchoredPosition = new Vector2(minigameScreen.sizeDelta.x * 0.5f, minigameScreen.sizeDelta.y * 0.5f);
    }

    protected override void Update()
    {
        if (fillImage == null)
            return;

        fillImage.fillAmount -= decreaseValue * Time.deltaTime;

        fillImage.color = Color.Lerp(minAmountColor, maxAmountColor, fillImage.fillAmount);

        // Player lost the game because the fillamount reached zero
        if (fillImage.fillAmount <= 0.0f)
        {
            player.audioSource.PlayOneShot(failSound);
            OnMinigameFail.Invoke();
        }
    }

    protected override void TearDown()
    {
        // Change controls back to player control
        player.controls.GuitarHeroMinigame.Trigger.started -= spamButtonPressedCallback;
        player.controls.GuitarHeroMinigame.Trigger.canceled -= spamButtonReleasedCallback;
        player.controls.SpamMinigame.Disable();
        player.controls.Player.Enable();
    }
}
