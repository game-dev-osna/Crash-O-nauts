using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProceduralCylinder))]
public class PlanetFiller : MonoBehaviour
{
    [SerializeField]
    public int m_Min=5;

    [SerializeField]
    public int m_Max=10;

    [SerializeField]
    public GameObject[] m_Prefabs;

    public void Init(int min, int max)
    {
        m_Min = min;
        m_Max = max;
    }

    public void FillPlanet()
    {
        GameObject parent = new GameObject("Stuff");

        parent.transform.SetParent(this.transform);
        parent.transform.localPosition = new Vector3(0f, -5f, 0f);

        int layerMask = 1 << 10;
        foreach (GameObject go in m_Prefabs)
        {
            int amount = Random.Range(m_Min, m_Max);

            for(int i=0; i< amount; i++)
            {
                GameObject inst = Instantiate(go, parent.transform);

                inst.transform.localPosition = Vector3.zero;
                inst.transform.Rotate(Random.Range(0f, 360f), 0f, 0f);
                inst.transform.Rotate(inst.transform.right, Random.Range(0f, 360f));

                inst.transform.localScale *= Random.Range(0.75f, 1.25f);

                RaycastHit result;

                Physics.Raycast(inst.transform.up * 10000f + inst.transform.position, inst.transform.up * -1f, out result, Mathf.Infinity, layerMask);

                inst.transform.position = result.point;
                inst.transform.Translate(new Vector3(2 + i * 2, -0.1f, -0.1f)) ;
            }
        }
        //destroy all child colliders after placing children
        Collider[] allColliders = parent.GetComponentsInChildren<Collider>();
        foreach (var childCollider in allColliders) Destroy(childCollider);
    }
}
