using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour
{
    public void ToggleSkin(int value)
    {
        GetComponent<Renderer>().material.mainTexture = PlayerSkinManager.Instance.Skins[value];
    }
}
