using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float jumpForce = 10f;
    public float groundCheckDistance = 0.2f;
    public float jumpCooldown = 4f;

    private float lastJumpTime;

    // Start is called before the first frame update
    void Start()
    {
        lastJumpTime = -jumpCooldown; // Set initial value to allow the first jump immediately
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the cooldown period has passed
        if (Time.time - lastJumpTime > jumpCooldown)
        {
            // Check if the enemy is grounded
            if (IsGrounded())
            {
                // Perform jump
                Jump();
            }
        }
    }

    bool IsGrounded()
    {
        // Cast a ray downwards to check if there's ground underneath
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);
        return hit.collider != null;
    }

    void Jump()
    {
        // Add force in the upward direction to make the enemy jump
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Update the last jump time
        lastJumpTime = Time.time;
    }
}
