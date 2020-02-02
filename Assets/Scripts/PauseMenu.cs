using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static PauseMenu _instance;

    public static PauseMenu Instance { get { return _instance; } }

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

    private bool isPaused = false;
    public GameObject pauseMenu;
    public AudioSource MenuOn;
    public AudioSource MenuOff;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        MenuOn.Play();
        AudioListener.volume *= 0.5f;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    private void ResumeGame()
    {
        MenuOff.Play();
        AudioListener.volume *= 2f;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ReturnToMain()
    {
        ResumeGame();
        GameManager.Instance.GameReset();
        //GameManager.Instance.ReturnToMenu();
    }
}
