using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] bool onLayer1 = false;

    public bool keepSpawning = true;

    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    
    public PlayerController playerController;

    float coolDown = 1f;

    public void spawnEnemy()
    {

        Vector2 spawnPoint = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 11.5f + (Vector2)transform.position;
        if(Random.Range(0,2) == 0)
        { 
            MeleeEnemy enemy = Instantiate(meleeEnemy, spawnPoint, Quaternion.identity).GetComponent<MeleeEnemy>();
            enemy.Init(playerController, onLayer1);
        }
        else
        {
            RangedEnemy enemy = Instantiate(rangedEnemy, spawnPoint, Quaternion.identity).GetComponent<RangedEnemy>();
            enemy.Init(playerController, onLayer1);
        }
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
