using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuttle : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;
    public ParticleSystem smokeParticleSystem;
    public ParticleSystem engineParticleSystem;

    public Transform root;
    public List<GameObject> shuttleParts;
    [HideInInspector]
    public List<Vector3> partStartLocalPositions = new List<Vector3>();
    [HideInInspector]
    public List<Quaternion> partStartLocalRotations = new List<Quaternion>();

    private void Awake()
    {
        foreach (GameObject shuttlePart in shuttleParts)
        {
            partStartLocalPositions.Add(shuttlePart.transform.localPosition);
            partStartLocalRotations.Add(shuttlePart.transform.localRotation);
        }
    }
}
