using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    float nextAttackTime = 0;
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField]float  attackCooldownNew = 1f;
    Vector3 facingDirection = Vector3.right;

    int prevDirection = -1;


    public override void Update()
    {
        base.Update();

        if(enemyActive)
        {
            int direction = Mathf.Clamp(4 - Mathf.RoundToInt(Vector2.SignedAngle(Vector2.right, targetPlayer.transform.position - transform.position) / 45), 0, 7);

            // Attacking
            if (attackCooldown < 0 && Vector3.Distance(targetPlayer.transform.position, transform.position) < attackRange)
            {
                StartCoroutine(ShootProjectile());
                attackCooldown = attackCooldownNew;

                animator.Play("scorpion_attack_" + direction);
            }
            attackCooldown -= Time.deltaTime;

            // Walk Animation
            if (prevDirection != direction)
            {
                prevDirection = direction;
                animator.Play("scorpion_walk_" + direction);
            }
        }
    }

    IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.2f);

        facingDirection = (targetPlayer.transform.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + facingDirection, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().Init(facingDirection, objectOnLayer1);

    }
}
