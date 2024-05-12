using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] bool onLayer1 = false;

    public bool keepSpawning = true;

    public GameObject[] enemyPrefabs;
    
    public PlayerController playerController;

    float coolDown = 1f;

    public void spawnEnemy()
    {

        Vector2 spawnPoint = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 11.5f + (Vector2)transform.position;
        Enemy enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint, Quaternion.identity).GetComponent<Enemy>();
        enemy.Init(playerController, onLayer1);
    }

    private void Update()
    {
        if (coolDown < 0f && keepSpawning)
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
