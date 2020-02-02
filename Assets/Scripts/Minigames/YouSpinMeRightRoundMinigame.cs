using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouSpinMeRightRoundMinigame : Minigame
{
    private float neededDegree = 1500.0f;
    private float offsetFactor = 20.0f;
    private float rotationSpeed = 100.0f;
    private Color minAmountColor = Color.red;
    private Color maxAmountColor = Color.green;
    private float decreaseValue = 60f;

    private AudioClip successSound;
    private AudioClip failSound;

    private float doneDegree;

    private Vector3 lastDir;
    private Sprite directionSprite;
    private RectTransform buttonPromptRectTransform;
    private Sprite squareSprite;
    private Sprite spinningSprite;
    private Image fillImage;
    private RectTransform spinningImageRectTransform;

    protected override void OneTimeSetup()
    {
        successSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Success");
        failSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Fail");

        squareSprite = Resources.Load<Sprite>("Images/Square");
        spinningSprite = Resources.Load<Sprite>("Images/Refresh");
    }

    protected override void SetupMinigame()
    {
        doneDegree = neededDegree / 2.0f;

        // Change controls to fit the minigame
        player.controls.Player.Disable();
        player.controls.SpinMinigame.Enable();
        player.controls.SpinMinigame.Movement.started += OnNewDirectionStarted;
        player.controls.SpinMinigame.Movement.performed += OnNewDirectionPressed;

        // Load button prompt
        directionSprite = Resources.Load<Sprite>("Images/Buttons/" + player.controlDevice.ToString() + "/Pad");

        GameObject fillImageGameObject = new GameObject("FillImage");
        RectTransform fillImageRectTransform = fillImageGameObject.AddComponent<RectTransform>();
        fillImage = fillImageGameObject.AddComponent<Image>();
        fillImage.sprite = squareSprite;
        fillImage.type = Image.Type.Filled;
        fillImage.fillMethod = Image.FillMethod.Vertical;
        fillImage.fillAmount = 0.5f;
        fillImageRectTransform.sizeDelta = minigameScreen.sizeDelta;
        fillImageRectTransform.anchorMax = Vector2.zero;
        fillImageRectTransform.anchorMin = Vector2.zero;
        fillImageRectTransform.SetParent(minigameScreen);
        fillImageRectTransform.localRotation = Quaternion.identity;
        fillImageRectTransform.localScale = Vector3.one;
        fillImageRectTransform.localPosition = Vector3.zero;
        fillImageRectTransform.anchoredPosition = new Vector2(minigameScreen.sizeDelta.x * 0.5f, minigameScreen.sizeDelta.y * 0.5f);

        GameObject spinningImageGameObject = new GameObject("Spinner");
        spinningImageRectTransform = spinningImageGameObject.AddComponent<RectTransform>();
        Image spinningImage = spinningImageGameObject.AddComponent<Image>();
        spinningImage.sprite = spinningSprite;
        spinningImageRectTransform.sizeDelta = new Vector2(minigameScreen.sizeDelta.y, minigameScreen.sizeDelta.y);
        spinningImageRectTransform.anchorMax = Vector2.zero;
        spinningImageRectTransform.anchorMin = Vector2.zero;
        spinningImageRectTransform.SetParent(minigameScreen);
        spinningImageRectTransform.localRotation = Quaternion.identity;
        spinningImageRectTransform.localScale = Vector3.one;
        spinningImageRectTransform.localPosition = Vector3.zero;
        spinningImageRectTransform.anchoredPosition = new Vector2(minigameScreen.sizeDelta.x * 0.5f, minigameScreen.sizeDelta.y * 0.5f);

        GameObject buttonPromptGameObject = new GameObject(directionSprite.name);
        buttonPromptRectTransform = buttonPromptGameObject.AddComponent<RectTransform>();
        Image buttonPromptImage = buttonPromptGameObject.AddComponent<Image>();
        buttonPromptImage.sprite = directionSprite;
        buttonPromptRectTransform.sizeDelta = new Vector2(minigameScreen.sizeDelta.y - 30, minigameScreen.sizeDelta.y - 30);
        buttonPromptRectTransform.anchorMax = Vector2.zero;
        buttonPromptRectTransform.anchorMin = Vector2.zero;
        buttonPromptRectTransform.SetParent(minigameScreen);
        buttonPromptRectTransform.localRotation = Quaternion.identity;
        buttonPromptRectTransform.localScale = Vector3.one;
        buttonPromptRectTransform.localPosition = Vector3.zero;
        buttonPromptRectTransform.anchoredPosition = new Vector2(minigameScreen.sizeDelta.x * 0.5f, minigameScreen.sizeDelta.y * 0.5f);
    }

    protected override void TearDown()
    {
        // Change controls to fit the minigame
        player.controls.SpinMinigame.Disable();
        player.controls.Player.Enable();
        player.controls.SpinMinigame.Movement.started -= OnNewDirectionStarted;
        player.controls.SpinMinigame.Movement.performed -= OnNewDirectionPressed;
    }

    private void OnNewDirectionStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        lastDir = context.ReadValue<Vector2>().normalized;
    }

    private void OnNewDirectionPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 currentDir = context.ReadValue<Vector2>();
        currentDir.Normalize();
        float signedAngle = Vector3.SignedAngle(lastDir, currentDir, -Vector3.forward);
        doneDegree += signedAngle;

        float percentage = doneDegree / neededDegree;
        fillImage.fillAmount = percentage;
        fillImage.color = Color.Lerp(minAmountColor, maxAmountColor, percentage);

        if (doneDegree >= neededDegree)
        {
            player.audioSource.PlayOneShot(successSound);
            OnMinigameSuccess.Invoke();
        }

        lastDir = currentDir;
    }

    protected override void Update()
    {
        Vector3 anchoredPosition = new Vector2(minigameScreen.sizeDelta.x * 0.5f, minigameScreen.sizeDelta.y * 0.5f);
        anchoredPosition += lastDir * offsetFactor;
        buttonPromptRectTransform.anchoredPosition = anchoredPosition;

        spinningImageRectTransform.Rotate(0, 0, -rotationSpeed * Time.deltaTime, Space.Self);

        doneDegree -= decreaseValue * Time.deltaTime;

        float percentage = doneDegree / neededDegree;
        fillImage.fillAmount = percentage;
        fillImage.color = Color.Lerp(minAmountColor, maxAmountColor, percentage);

        // Player lost the game because the fillamount reached zero
        if (fillImage.fillAmount <= 0.0f)
        {
            player.audioSource.PlayOneShot(failSound);
            OnMinigameFail.Invoke();
        }
    }
}
