using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Enemy : ColorfulObject
{
    public int damageAmount = 10;
    public float attackCooldown = 1f;
    public float attackRange = 1f;

    [HideInInspector] public PlayerController targetPlayer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool enemyActive = true;
    [SerializeField] float EnemySpeed = 1f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] GameObject dustParticles;

    Rigidbody2D rb2;
    Collider2D col;
    int currentHealth;

    public virtual void Init(PlayerController targetPlayer, bool onLayer1 = false)
    {
        rb2 = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        this.targetPlayer = targetPlayer;

        if(onLayer1)
        {
            objectOnLayer1 = true;
            rb2.excludeLayers = 0b01000000;

            gameObject.layer = 7;
            foreach (Transform child in transform)
            {
                child.gameObject.layer = 7;
            }
        }
        else
        {
            objectOnLayer1 = false;
            rb2.excludeLayers = 0b10000000;

            gameObject.layer = 6;
            foreach (Transform child in transform)
            {
                child.gameObject.layer = 6;
            }
        }

        // Random Main Color
        int randomColor = Random.Range(1, 8);
        colorRed = (randomColor & 0b100) == 0b100;
        colorGreen = (randomColor & 0b010) == 0b010;
        colorBlue = (randomColor & 0b001) == 0b001;
    }

    public override void Start()
    {
        base.Start();

        ColorChanged();
    }

    public override void Update()
    {
        base.Update();

        if(enemyActive)
        {
            Vector3 moveDirection = (targetPlayer.transform.position - transform.position);

            if (moveDirection.magnitude < attackRange - 1)
                moveDirection *= -1;

            rb2.velocity = moveDirection.normalized * EnemySpeed;
            if(animator) animator.speed = 1;
        }
        else
        {
            rb2.velocity = Vector3.zero;
            if (animator) animator.speed = 0;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);

        ColorManager.instance.onColorChanged -= ColorChanged;

        Instantiate(dustParticles, transform.position, Quaternion.identity).layer = objectOnLayer1 ? 7 : 6;
    }

    public override void ColorChanged()
    {
        base.ColorChanged();

        col.enabled = objectVisible;
        enemyActive = objectVisible;

        renderer.sortingOrder = (objectVisible ? 5 : 4);
    }
}