using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Text>().text = GameManager.Instance.totalTime.ToString();
    }
    
}
