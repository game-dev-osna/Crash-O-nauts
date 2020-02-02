using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Debug class to play a specified minigame on load
/// </summary>
public class MinigamePlayer : MonoBehaviour
{
    [SerializeField] private Player player;

    private Minigame minigame;

    void Start()
    {
        //minigame = new QTEMinigame();
        //minigame = new GuitarHeroMinigame();
        minigame = new SpamMinigame();

        minigame.InitMinigame(player);
        minigame.OnMinigameSuccess.AddListener(OnSucess);
        minigame.OnMinigameFail.AddListener(OnFail);
    }

    private void Update()
    {
        minigame.UpdateMinigame();
    }

    private void OnSucess()
    {
        Debug.Log("Success");

        minigame.EndMinigame();
    }

    private void OnFail()
    {
        Debug.Log("Fail");

        minigame.EndMinigame();
    }
}
