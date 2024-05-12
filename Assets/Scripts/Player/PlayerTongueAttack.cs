using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTongueAttack : MonoBehaviour
{
    public int damage = 20;
    public int layer = 0;

    [SerializeField] GameObject tongueParticles;
    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void AttackDirection(Vector3 direction)
    {
        StartCoroutine(TongueAttack(direction));
    }

    IEnumerator TongueAttack(Vector3 direction)
    {
        transform.right = direction;
        renderer.size = new Vector3(0, 0.25f);

        float nearestDistance = 4;
        Enemy nearestEnemy = null;

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            float enmDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if ((enemy.gameObject.layer == layer + 6) && Vector3.Dot((enemy.transform.position - transform.position).normalized, direction) > 0.9f && enmDistance < nearestDistance && enemy.enemyActive)
            {
                nearestDistance = enmDistance;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy)
        {
            nearestEnemy.TakeDamage(damage);

            transform.right = nearestEnemy.transform.position - transform.position;
            renderer.size = new Vector3(nearestDistance, 0.25f);

            // weird particles
            GameObject obj = Instantiate(tongueParticles, nearestEnemy.transform.position, Quaternion.identity);
            obj.GetComponent<ParticleSystem>().startColor = nearestEnemy.renderer.color;
            obj.layer = gameObject.layer;

            SfxPlayer.instance.PlaySound("lick_hit");
        }
        else
        {
            renderer.size = new Vector3(4, 0.25f);

            SfxPlayer.instance.PlaySound("lick");
        }

        yield return new WaitForSeconds(0.1f);

        renderer.size = new Vector3(0, 0.25f);
    }

    private void OnCollisionTrigger2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out Enemy enemyHealth))
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);

        //Instantiate(bulletHitParticles, transform.position, Quaternion.identity).layer = gameObject.layer;
    }
}
