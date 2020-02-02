using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    private static PlayerSkinManager _instance;

    public static PlayerSkinManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
            m_Selection = new int[4];

            for(int i=0; i< m_Selection.Length; i++)
            {
                m_Selection[i] = Random.Range(0, m_Skins.Length - 1);
            }

            m_Colors = new Color[5];

            m_Colors[0] = new Color(100 / 255f, 143 / 255f, 255 / 255f);
            m_Colors[1] = new Color(120 / 255f, 94 / 255f, 240 / 255f);
            m_Colors[2] = new Color(220 / 255f, 38 / 255f, 127 / 255f);
            m_Colors[3] = new Color(254 / 255f, 97 / 255f, 0 / 255f);
            m_Colors[4] = new Color(255 / 255f, 176 / 255f, 0 / 255f);
        }
    }

    [SerializeField]
    private Texture2D[] m_Skins;

    public Texture2D[] Skins { get { return this.m_Skins; } }

    private Color[] m_Colors;

    public Color[] Colors { get { return this.m_Colors; } }


    private int[] m_Selection;

    public int ToggleSelection(int id)
    {
        m_Selection[id] = (m_Selection[id] + 1) % Skins.Length;

        return m_Selection[id];
    }

    public int GetSelection(int id)
    {
        return m_Selection[id];
    }
}
