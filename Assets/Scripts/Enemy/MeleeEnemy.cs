using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] GameObject flyAttackParticles;
    [SerializeField] Sprite[] flySprites;
    float nextAttackTime = 0;

    public override void Update()
    {
        base.Update();

        // Attacking
        if (enemyActive && Time.time > nextAttackTime && Vector3.Distance(targetPlayer.transform.position, transform.position) < attackRange)
        {
            // If the player is in attack range, lower its health

            targetPlayer.TakeDamage(damageAmount, transform.position);
            nextAttackTime = Time.time + attackCooldown;

            // Particles
            Instantiate(flyAttackParticles, transform.position, Quaternion.identity).layer = objectOnLayer1 ? 7 : 6;
        }

        // Sprite Changing
        if(enemyActive)
            renderer.sprite = flySprites[Mathf.Clamp(4 - Mathf.RoundToInt(Vector2.SignedAngle(Vector2.right, targetPlayer.transform.position - transform.position) / 45), 0, 7)];
    }

    public override void Die()
    {
        base.Die();

        SfxPlayer.instance.PlaySound("fly_death");
    }
}
