using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ColorfulObject
{
    public int damageAmount = 10;
    public float attackCooldown = 1f;
    public float attackRange = 1f;

    [HideInInspector] public PlayerController targetPlayer;
    [SerializeField] float EnemySpeed = 1f;
    [SerializeField] int maxHealth = 100;

    Rigidbody2D rb2;
    int currentHealth;

    public virtual void Init(PlayerController targetPlayer, bool onLayer1 = false)
    {
        rb2 = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        this.targetPlayer = targetPlayer;

        if(onLayer1)
        {
            objectOnLayer1 = true;
            gameObject.layer = 7;
            rb2.excludeLayers = 0b1000000;
        }
        else
        {
            objectOnLayer1 = false;
            gameObject.layer = 6;
            rb2.excludeLayers = 0b0100000;
        }

        // Random Color
        switch(Random.Range(0, 3))
        {
            case 0:
                colorRed = false;
                break;
            case 1:
                colorGreen = false; 
                break;
            case 2:
                colorBlue = false;
                break;
        }
    }

    public override void Start()
    {
        base.Start();

        ColorChanged();
    }

    public override void Update()
    {
        base.Update();

        Vector3 moveDirection = (targetPlayer.transform.position - transform.position);

        if (moveDirection.magnitude < attackRange)
            moveDirection *= -1;

        rb2.velocity = moveDirection.normalized * EnemySpeed;

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);

            ColorManager.instance.onColorChanged -= ColorChanged;
        }
    }

    public override void ColorChanged()
    {
        base.ColorChanged();

        gameObject.SetActive(objectVisible);
    }
}