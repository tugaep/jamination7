using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedEnemy : Enemy
{
    float nextAttackTime = 0;
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField]float  attackCooldownNew = 1f;
    Vector3 facingDirection = Vector3.right;

    public override void Update()
    {
        base.Update();

        if(enemyActive)
        {
            // Attacking
            if (attackCooldown < 0 && Vector3.Distance(targetPlayer.transform.position, transform.position) < attackRange)
            {
                Attack();
                attackCooldown = attackCooldownNew;

            }
            attackCooldown -= Time.deltaTime;
        }
    }

    public virtual void Attack()
    {
        StartCoroutine(ShootProjectile());
    }

    IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.2f);

        facingDirection = (targetPlayer.transform.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + facingDirection, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().Init(facingDirection, objectOnLayer1);

    }
}
