using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionEnemy : RangedEnemy
{
    int direction = 0;
    int prevDirection = -1;

    public override void Update()
    {
        direction = Mathf.Clamp(4 - Mathf.RoundToInt(Vector2.SignedAngle(Vector2.right, targetPlayer.transform.position - transform.position) / 45), 0, 7);

        base.Update();

        // Walk Animation
        if (prevDirection != direction)
        {
            prevDirection = direction;
            animator.Play("scorpion_walk_" + direction);
        }
    }

    public override void Attack()
    {
        base.Attack();

        animator.Play("scorpion_attack_" + direction);
    }
}
