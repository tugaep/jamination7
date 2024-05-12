using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowedBackground : MonoBehaviour
{
    public Transform target;

    public float xStep = 27f;
    public float yStep = 16f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Round(target.position.x / xStep) * xStep, Mathf.Round(target.position.y / yStep) * yStep, 0);
    }
}
