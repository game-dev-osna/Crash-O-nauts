using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetOrAddComponent<T>(this GameObject value) where T : MonoBehaviour
    {
        T component = value.GetComponent<T>();
        if (component != null)
            return component;
        else
            return value.AddComponent<T>();
    }
}
