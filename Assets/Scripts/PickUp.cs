using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int index;
    public Minigame minigame;

    private void Start()
    {
        switch (index)
        {
            case 0:
                minigame = new QTEMinigame();
                break;
            case 1:
                minigame = new GuitarHeroMinigame();
                break;
            case 2:
                minigame = new SpamMinigame();
                break;
            case 3:
                minigame = new YouSpinMeRightRoundMinigame();
                break;
            default:
                Debug.Log("This should not be called");
                minigame = new QTEMinigame();
                break;
        }
    }

    private void Update()
    {
        if (minigame.isActive)
            minigame.UpdateMinigame();
    }
}
