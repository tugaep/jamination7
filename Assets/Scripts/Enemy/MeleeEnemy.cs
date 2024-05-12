using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class MeleeEnemy : Enemy
{
    float nextAttackTime = 0;

    public override void Update()
    {
        base.Update();

        // Attacking
        if (enemyActive && Time.time > nextAttackTime && Vector3.Distance(targetPlayer.transform.position, transform.position) < attackRange)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        // If the player is in attack range, lower its health

        targetPlayer.TakeDamage(damageAmount, transform.position);
        nextAttackTime = Time.time + attackCooldown;
    }

}
