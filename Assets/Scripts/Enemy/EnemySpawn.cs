using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] bool onLayer1 = false;

    public GameObject meleeEnemy;

    public PlayerController playerController;

    float coolDown = 5f;

    public void spawnEnemy()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 8.5f + (Vector2)transform.position;

        MeleeEnemy enemy = Instantiate(meleeEnemy, spawnPoint, Quaternion.identity).GetComponent<MeleeEnemy>();
        enemy.Init(playerController, onLayer1);
    }

    private void Update()
    {
        if (coolDown < 0f)
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
