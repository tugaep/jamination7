using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;


    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, 4 * Time.deltaTime),
            Mathf.Lerp(transform.position.y, target.position.y, 4 * Time.deltaTime),-10f);
    }
}
