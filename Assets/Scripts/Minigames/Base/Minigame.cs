using UnityEngine;
using UnityEngine.Events;

public abstract class Minigame
{
    public bool isActive;

    protected RectTransform minigameScreen;
    protected Player player;

    private int playerIndex;

    // Events to call after the minigame is done
    public UnityEvent OnMinigameSuccess = new UnityEvent();
    public UnityEvent OnMinigameFail    = new UnityEvent();

    public Minigame()
    {
        OneTimeSetup();
    }

    /// <summary>
    /// Load resources needed for the minigame
    /// </summary>
    protected abstract void OneTimeSetup();

    public void InitMinigame(Player player)
    {
        OnMinigameSuccess.RemoveAllListeners();
        OnMinigameFail.RemoveAllListeners();

        minigameScreen = player.minigameScreen;
        minigameScreen.gameObject.SetActive(true);
        this.player = player;
        isActive = true;

        SetupMinigame();
    }

    /// <summary>
    /// Setup the minigame to player 
    /// </summary>
    protected abstract void SetupMinigame();



    public void UpdateMinigame()
    {
        if (isActive)
            Update();
    }


    protected abstract void Update();

    public void EndMinigame()
    {
        isActive = false;

        // Clear the minigame screen
        for (int i = 0; i < minigameScreen.childCount; i++)
            Object.Destroy(minigameScreen.GetChild(i).gameObject);
        minigameScreen.gameObject.SetActive(false);

        TearDown();
    }

    /// <summary>
    /// Remove all used minigame resources
    /// </summary>
    protected abstract void TearDown();
}
