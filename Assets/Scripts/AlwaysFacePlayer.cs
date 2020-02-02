using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFacePlayer : MonoBehaviour
{
    private Vector3 startLocalPosition;

    private void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = startLocalPosition;
        Vector3 uiPos = transform.position;
        uiPos.x += -10;
        transform.position = uiPos;

        Vector3 target = transform.position;
        target.x += 1000f;
        transform.LookAt(target, transform.parent.up);
    }
}
