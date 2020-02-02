using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private static MenuController _instance;

    public static MenuController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public AudioSource startSound;

    [SerializeField]
    private string game;

    public void StartGameSound()
    {
        startSound.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
