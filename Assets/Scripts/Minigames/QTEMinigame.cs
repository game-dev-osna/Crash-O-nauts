using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A short quick time event minigame, where the player needs to press a specific button combination
/// </summary>
public class QTEMinigame : Minigame
{
    // Change difficulty
    private int combinationLength = 5;

    private List<int> buttonCombination = new List<int>();
    private List<Sprite> buttonSprites = new List<Sprite>();
    private List<GameObject> buttonObjects = new List<GameObject>();
    private int currentIndex;
    private List<Action<UnityEngine.InputSystem.InputAction.CallbackContext>> buttonFunctions = new List<Action<UnityEngine.InputSystem.InputAction.CallbackContext>>();

    private AudioClip correctSound;
    private AudioClip incorrectSound;
    private AudioClip successSound;
    private AudioClip failSound;

    private bool firstFrame = true;

    private void OnQTEButtonPressed(int buttonIndex)
    {
        if (firstFrame)
            return;

        // Pressed right button
        if(buttonCombination[currentIndex] == buttonIndex)
        {
            Color currentColor = buttonObjects[currentIndex].GetComponent<Image>().color;
            Color finishedColor = Color.white * currentColor;
            finishedColor.a = 0.5f;
            buttonObjects[currentIndex].GetComponent<Image>().color = finishedColor;

            currentIndex++;
            if(currentIndex == buttonCombination.Count)
            {
                player.audioSource.PlayOneShot(successSound);
                OnMinigameSuccess.Invoke();
            }
            else
            {
                player.audioSource.PlayOneShot(correctSound);
            }
        }
        // Pressed wrong button 
        else
        {
            player.audioSource.PlayOneShot(failSound);
            OnMinigameFail.Invoke();
        }
    }

    protected override void OneTimeSetup()
    {
        correctSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Correct");
        successSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Success");
        failSound = Resources.Load<AudioClip>("SFX/MinigameSounds/Fail");
    }

    protected override void SetupMinigame()
    {
        Debug.Log("Setup");

        buttonSprites.Clear();
        // Load sprites
        foreach (Sprite buttonSprite in Resources.LoadAll<Sprite>("Images/Buttons/" + player.controlDevice.ToString() + "/Actions"))
            buttonSprites.Add(buttonSprite);

        currentIndex = 0;

        // Init button array to execute
        System.Random random = new System.Random();
        for (int i = 0; i < combinationLength; i++)
        {
            int randomButton = random.Next(0, 3);
            buttonCombination.Add(randomButton);
        }

        // Debug combination
        string combination = "Combination: ";
        foreach (int buttonIndex in buttonCombination)
            combination += (buttonIndex + 1) + " ";
        Debug.Log(combination);

        // Change controls to fit the minigame
        player.controls.Player.Disable();
        player.controls.QTEMinigame.Enable();

        // Setup callbacks
        for (int i = 0; i < player.controls.QTEMinigame.Get().actions.Count; i++)
        {
            int index = new int();
            index = i;
            Action<UnityEngine.InputSystem.InputAction.CallbackContext> buttonCallback = delegate { OnQTEButtonPressed(index); };
            buttonFunctions.Add(buttonCallback);
            player.controls.QTEMinigame.Get().actions[i].started += buttonCallback;
        }

        float spriteSize = minigameScreen.sizeDelta.x / buttonCombination.Count;

        // Instantiate sprite objects
        int offsetIndex = 0;
        foreach (int buttonIndex in buttonCombination)
        {
            Sprite sprite = buttonSprites[buttonIndex];
            GameObject spriteObject = new GameObject(sprite.name);
            RectTransform spriteObjectRectTransform = spriteObject.AddComponent<RectTransform>();
            Image spriteImage = spriteObject.AddComponent<Image>();
            spriteImage.sprite = sprite;
            spriteObjectRectTransform.sizeDelta = new Vector2(spriteSize, spriteSize);
            spriteObjectRectTransform.anchorMax = Vector2.zero;
            spriteObjectRectTransform.anchorMin = Vector2.zero;
            spriteObjectRectTransform.SetParent(minigameScreen);
            spriteObjectRectTransform.localRotation = Quaternion.identity;
            spriteObjectRectTransform.localScale = Vector3.one;
            spriteObjectRectTransform.localPosition = Vector3.zero;
            spriteObjectRectTransform.anchoredPosition = new Vector2(spriteSize / 2 + offsetIndex * spriteSize, minigameScreen.sizeDelta.y * 0.5f);
            buttonObjects.Add(spriteObject);

            offsetIndex++;
        }
    }

    protected override void Update() 
    {
        firstFrame = false;
    }

    protected override void TearDown()
    {
        Debug.Log("Tear down");

        // Reassign action set
        player.controls.QTEMinigame.Disable();
        player.controls.Player.Enable();

        // Clear the button combination
        buttonCombination.Clear();
        buttonObjects.Clear();

        // Remove callbacks
        for (int i = 0; i < buttonFunctions.Count; i++)
        {
            Action<UnityEngine.InputSystem.InputAction.CallbackContext> buttonCallback = buttonFunctions[i];
            player.controls.QTEMinigame.Get().actions[i].started -= buttonCallback;
        }

        buttonFunctions.Clear();
    }
}
