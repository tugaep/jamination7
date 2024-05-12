using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MeleeEnemy
{
    [SerializeField] GameObject snakeAttackParticles;

    int direction = 0;
    int prevDirection = -1;

    public override void Update()
    {
        direction = Mathf.Clamp(4 - Mathf.RoundToInt(Vector2.SignedAngle(Vector2.right, targetPlayer.transform.position - transform.position) / 45), 0, 7);

        base.Update();

        // Walk Animation
        if (prevDirection != direction && enemyActive)
        {
            prevDirection = direction;
            animator.Play("snake_" + direction);
        }
    }

    public override void Attack()
    {
        base.Attack();

        Instantiate(snakeAttackParticles, targetPlayer.transform.position, Quaternion.identity).layer = gameObject.layer;
    }

    public override void Die()
    {
        base.Die();

        SfxPlayer.instance.PlaySound("snake_death");
    }
}
