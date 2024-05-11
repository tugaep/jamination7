using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] bool onLayer1 = false;
    public GameObject speedGem;

    public GameObject attackGem;

    public GameObject healGem;

    float coolDown = 5f;


    public void spawnGem()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 11.5f + (Vector2)transform.position;
        int random = Random.Range(0, 3);

        if(random == 0)
        {
            SpeedGem gem = Instantiate(speedGem, spawnPoint, Quaternion.identity).GetComponent<SpeedGem>();
            gem.Init(onLayer1);
        }
        else if(random == 1 )
        {
            AttackSpeedGem gem = Instantiate(attackGem, spawnPoint, Quaternion.identity).GetComponent<AttackSpeedGem>();
            gem.Init(onLayer1);
        }
        else
        {
            HealthGem gem = Instantiate(healGem, spawnPoint, Quaternion.identity).GetComponent<HealthGem>();
            gem.Init(onLayer1);
        }
    }



    private void Update()
    {
        if (coolDown < 0f)
        {
            spawnGem();
            coolDown = 5f;
        }
        else
        {
            coolDown -= Time.deltaTime;
        }

    }
}
