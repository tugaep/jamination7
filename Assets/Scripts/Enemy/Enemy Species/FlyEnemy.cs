using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MeleeEnemy
{

    [SerializeField] GameObject flyAttackParticles;
    [SerializeField] Sprite[] flySprites;

    public override void Attack()
    {
        base.Attack();

        // Particles
        Instantiate(flyAttackParticles, transform.position, Quaternion.identity).layer = objectOnLayer1 ? 7 : 6;
    }

    public override void Update()
    {
        base.Update();

        // Sprite Changing
        if (enemyActive)
            renderer.sprite = flySprites[Mathf.Clamp(4 - Mathf.RoundToInt(Vector2.SignedAngle(Vector2.right, targetPlayer.transform.position - transform.position) / 45), 0, 7)];
    }

    public override void Die()
    {
        base.Die();

        SfxPlayer.instance.PlaySound("fly_death");
    }
}
