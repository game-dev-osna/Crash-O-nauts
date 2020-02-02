using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public void SetColor(Color value)
    {
        Material material = GetComponent<Renderer>().materials[0];
        material.color = value;

        Material[] mats = new Material[GetComponent<Renderer>().materials.Length];

        mats[0] = material;


        for (int i = 1; i<mats.Length-3; i++)
        {
            mats[i] = GetComponent<Renderer>().materials[i];
        }

        material = Instantiate(material);
        material.color = Color.white;

        for (int i = mats.Length - 3; i < mats.Length - 1; i++)
        {
            mats[i] = material;
        }

        mats[mats.Length-1] = GetComponent<Renderer>().materials[mats.Length - 1];

        GetComponent<Renderer>().materials = mats;

    }
}
