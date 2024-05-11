using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;

    public Transform camera;

    float coolDown = 5f;

    public void spawnEnemy()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 8.5f + (Vector2)camera.position;
        Instantiate(enemy, spawnPoint, Quaternion.identity);

    }
    private void Update()
    {
        if (coolDown == 0f)
        {
            spawnEnemy();
            coolDown = 5f;
        }
        else
        {
            coolDown -= Time.deltaTime;
        }



    }
}
