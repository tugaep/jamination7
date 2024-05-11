using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerLayer = 0;
    [SerializeField] float playerSpeed = 1.0f;

    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    [SerializeField] GameObject bulletPrefab;

    Rigidbody2D rb2;
    Vector3 facingDirection = Vector3.right;

    // Input names
    string axisHorName;
    string axisVertName;
    string inputAttackName;

    float attackCooldown = 0f;
   
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        axisHorName = $"P{(playerLayer + 1)}Horizontal";
        axisVertName = $"P{(playerLayer + 1)}Vertical";
        inputAttackName = $"P{(playerLayer + 1)}Attack";
    }

    void Update()
    {
        // Player Movement
        float axisHorizontal = Input.GetAxis(axisHorName);
        float axisVertical = Input.GetAxis(axisVertName);

        Vector3 direction = new Vector2(axisHorizontal, axisVertical);
        rb2.velocity = direction * playerSpeed;

        if(direction.sqrMagnitude > 0)
            facingDirection = direction.normalized;

        // Player Attacking
        attackCooldown -= Time.deltaTime;
        if (Input.GetButton(inputAttackName) && attackCooldown < 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + facingDirection, Quaternion.identity);
            bullet.GetComponent<PlayerBullet>().Init(facingDirection, playerLayer == 1);

            attackCooldown = 0.1f;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Debug.Log("Player has died.");
        }
    }
}
