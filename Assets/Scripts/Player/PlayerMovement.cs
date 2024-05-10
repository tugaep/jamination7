using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] string inputPlayerId = "P1";
    [SerializeField] float playerSpeed = 1.0f;

    Rigidbody2D rb2;

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        float axisHorizontal = Input.GetAxis(inputPlayerId + "Horizontal");
        float axisVertical = Input.GetAxis(inputPlayerId + "Vertical");
        
        rb2.velocity = new Vector2 (axisHorizontal, axisVertical) * playerSpeed;
    }
}
