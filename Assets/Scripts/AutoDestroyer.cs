using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    [SerializeField] float delay;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
