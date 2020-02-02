using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    
    public float xAxis;
    public float yAxis;
    public float zAxis;
    public AudioSource sound;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xAxis * Time.deltaTime, yAxis * Time.deltaTime, zAxis * Time.deltaTime);
    }

    public void ReturnToMain()
    {
        sound.Play();
        GameManager.Instance.GameReset();
    }
}
