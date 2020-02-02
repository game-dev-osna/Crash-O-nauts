using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    GameManager manager;

    private void Awake()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TMP_Text>().text = (manager.maxTime - Mathf.Round(manager.timer)).ToString() + "s";
    }
}
