using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] Sprite[] flySprites;
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

        // Sprite Changing
        if(enemyActive)
            renderer.sprite = flySprites[Mathf.Clamp(4 - Mathf.RoundToInt(Vector2.SignedAngle(Vector2.right, targetPlayer.transform.position - transform.position) / 45), 0, 7)];
    }
}
