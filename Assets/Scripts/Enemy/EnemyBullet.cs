using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] GameObject bulletHitParticles;
    [SerializeField] int damage = 20;
    [SerializeField] float bulletSpeed = 6f;

    Vector3 moveDirection = Vector3.up;
    float deathTime;

    Rigidbody2D rb2;
    public void Init(Vector3 bulletDirection, bool onLayer1 = false)
    {
        rb2 = GetComponent<Rigidbody2D>();

        moveDirection = bulletDirection.normalized;
        deathTime = Time.time + 5;

        if (onLayer1)
        {
            gameObject.layer = 7;
            rb2.excludeLayers = 0b1000000;
        }
        else
        {
            gameObject.layer = 6;
            rb2.excludeLayers = 0b0100000;
        }
    }
    private void Update()
    {
        if (deathTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb2.velocity = moveDirection * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);

        Instantiate(bulletHitParticles, transform.position, Quaternion.identity).layer = gameObject.layer;
    }
}


