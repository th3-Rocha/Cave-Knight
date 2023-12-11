using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float movementSpeed = 3f; // Adjust the speed as needed
    private Rigidbody2D rb;
    private float timer = 0f;
    private bool isMovingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move left or right based on the direction flag
        float horizontalMovement = isMovingRight ? 1f : -1f;
        rb.velocity = new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);

        // Update the timer
        timer += Time.deltaTime;

        // Change direction after 2 seconds
        if (timer >= 2f)
        {
            isMovingRight = !isMovingRight;
            timer = 0f; // Reset the timer
        }
    }
}
