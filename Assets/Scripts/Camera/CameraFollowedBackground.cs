using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowedBackground : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Round(target.position.x / 4) * 4, Mathf.Round(target.position.y / 4) * 4, 0);
    }
}
