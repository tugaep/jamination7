using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] string inputPlayerId = "P1";
    [SerializeField] float playerSpeed = 1.0f;

    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletForce = 20f;

    Rigidbody2D rb2;
   
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetButtonDown(inputPlayerId + "Attack"))
        {
            Vector3 shootDirection = transform.forward;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                bulletRigidbody.AddForce(shootDirection * bulletForce, ForceMode.Impulse);
            }
        }
        float axisHorizontal = Input.GetAxis(inputPlayerId + "Horizontal");
        float axisVertical = Input.GetAxis(inputPlayerId + "Vertical");

        rb2.velocity = new Vector2(axisHorizontal, axisVertical) * playerSpeed;
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
