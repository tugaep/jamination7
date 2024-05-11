using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform target;


    private void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, Time.deltaTime),
            Mathf.Lerp(transform.position.y, target.position.y, Time.deltaTime),-10f);
    }
}
