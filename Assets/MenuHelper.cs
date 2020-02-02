using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHelper : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Story", LoadSceneMode.Single);
    }
}
