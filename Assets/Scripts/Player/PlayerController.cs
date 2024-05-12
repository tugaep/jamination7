using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerLayer = 0;
    [SerializeField] float playerSpeed = 1.0f;

    public int maxHealth = 100;
    public int currentHealth;

    public bool playerActive = true;

    [SerializeField] PlayerTongueAttack tongue;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] GameObject effectSpeed;
    [SerializeField] GameObject effectHeal;
    [SerializeField] GameObject effectAttack;

    Rigidbody2D rb2;
    Vector3 facingDirection = Vector3.right;

    // Input names
    string axisHorName;
    string axisVertName;
    string inputAttackName;

    float attackCooldown = 0f;

    float attackSpeed = 0.5f;
   
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
        if(playerActive)
        {
            // Player Movement
            float axisHorizontal = Input.GetAxis(axisHorName);
            float axisVertical = Input.GetAxis(axisVertName);

            Vector3 direction = new Vector2(axisHorizontal, axisVertical);
            rb2.velocity = direction.normalized * playerSpeed;

            // Clamping Player Position
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30, 30), Mathf.Clamp(transform.position.y, -30, 30), 0);

            if (direction.sqrMagnitude > 0)
                facingDirection = direction.normalized;

            // Player Attacking
            attackCooldown -= Time.deltaTime;
            if (Input.GetButtonDown(inputAttackName) && attackCooldown < 0)
            {
                /*GameObject bullet = Instantiate(bulletPrefab, transform.position + facingDirection, Quaternion.identity);
                bullet.GetComponent<PlayerBullet>().Init(facingDirection * playerSpeed / 3, playerLayer == 1);*/

                attackCooldown = attackSpeed;

                tongue.AttackDirection(facingDirection);
            }
        }
    }

    public void TakeDamage(int amount, Vector3? damageSource = null)
    {
        // Knockback
        if (damageSource != null)
            rb2.position += (Vector2)(transform.position - damageSource.Value).normalized;

        // Damaging
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            if(playerActive)
            {
                FindObjectOfType<GameOverSequence>().TerminateGame(playerLayer == 1);
            }

            rb2.velocity = Vector2.zero;
            playerActive = false;

        }

        //Audio
        SfxPlayer.instance.PlaySound("damage");
    }

    public IEnumerator IncreaseSpeed()
    {
        playerSpeed += 1f;

        Instantiate(effectSpeed, transform).layer = gameObject.layer;

        yield return new WaitForSeconds(5);

        playerSpeed -= 1f;
    }

    public void Heal()
    {
        currentHealth += 10;

        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        Instantiate(effectHeal, transform.position, Quaternion.identity).layer = gameObject.layer;
    }
    public IEnumerator attackSpeedModifiy()
    {
        attackSpeed /= 1.2f;

        Instantiate(effectAttack, transform).layer = gameObject.layer;

        yield return new WaitForSeconds(5);

        attackSpeed *=  1.2f;
    }
}
