using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform targetPlayer;
    [SerializeField] float EnemySpeed = 1f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    [SerializeField] float attackRange;
    [SerializeField] int damageAmount = 10;
    [SerializeField] float attackCooldown;


    private float nextAttackTime;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        transform.position += (targetPlayer.position - transform.position).normalized * EnemySpeed * Time.deltaTime;

        if(Time.time > nextAttackTime)
        {

            //Igrenc oldu farkindayim
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Player"))
                {
                    
                    AttackPlayer(collider.gameObject);
                    nextAttackTime = Time.time + attackCooldown;
                    break; 
                }
            }
        }

    }
    private void AttackPlayer(GameObject player)
    {
        
        PlayerMovement playerHealth = player.GetComponent<PlayerMovement>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}