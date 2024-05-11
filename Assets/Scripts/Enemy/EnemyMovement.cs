using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform targetPlayer;

    [SerializeField] float EnemySpeed = 1f;

    void Update()
    {
        transform.position += (targetPlayer.position - transform.position).normalized * EnemySpeed * Time.deltaTime;

    }
}