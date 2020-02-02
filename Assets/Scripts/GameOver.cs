using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioSource sound;

    public void ReturnToMain()
    {
        sound.Play();
        GameManager.Instance.GameReset();
    }
}
