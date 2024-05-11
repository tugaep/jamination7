using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    float nextAttackTime = 0;

    public override void Update()
    {
        base.Update();

        // Attacking
        if (enemyActive && Time.time > nextAttackTime && Vector3.Distance(targetPlayer.transform.position, transform.position) < attackRange)
        {
            // If the player is in attack range, lower its health

            targetPlayer.TakeDamage(damageAmount);
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
