using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    float nextAttackTime = 0;
    


    [SerializeField] GameObject bulletPrefab;
    [SerializeField]float  attackCooldownNew = 1f;
    Vector3 facingDirection = Vector3.right;


    public override void Update()
    {
        base.Update();

        
        if (attackCooldown < 0 && Vector3.Distance(targetPlayer.transform.position, transform.position) < attackRange)
        {
            facingDirection = (targetPlayer.transform.position - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position + facingDirection, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().Init(facingDirection, objectOnLayer1);

            attackCooldown = attackCooldownNew;
            print("SSASASASA");
        }
        attackCooldown -= Time.deltaTime;


    }
}
