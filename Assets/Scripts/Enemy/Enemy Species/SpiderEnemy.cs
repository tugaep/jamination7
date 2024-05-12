using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : MeleeEnemy
{
    [SerializeField] GameObject spiderAttackParticles;

    [SerializeField] Sprite[] spiderSprites;
   

    public override void Attack()
    {
        base.Attack();

        // Particles
        Instantiate(spiderAttackParticles, transform.position, Quaternion.identity).layer = objectOnLayer1 ? 7 : 6;
    }


    public override void Update()
    {
        base.Update();

        // Sprite Changing
        if (enemyActive)
            renderer.sprite = spiderSprites[Mathf.Clamp(4 - Mathf.RoundToInt(Vector2.SignedAngle(Vector2.right, targetPlayer.transform.position - transform.position) / 45), 0, 7)];
    }

    public override void Die()
    {
        base.Die();

        SfxPlayer.instance.PlaySound("spider_death");
    }

}
