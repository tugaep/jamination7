using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    [SerializeField] float boundaryX0;
    [SerializeField] float boundaryX1;
    [SerializeField] float boundaryY0;
    [SerializeField] float boundaryY1;

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.position.x, target.position.x, 4 * Time.deltaTime), boundaryX0, boundaryX1),
                                        Mathf.Clamp(Mathf.Lerp(transform.position.y, target.position.y, 4 * Time.deltaTime), boundaryY0, boundaryY1),
                                        -10f);
    }
}
