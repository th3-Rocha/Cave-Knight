using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float movementSpeed = 5f; // Adjust the speed as needed
    public float detectionRange = 10f; // Adjust the range as needed
    private Transform player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
             
                Vector2 direction = new Vector2(player.position.x - transform.position.x, 0f).normalized;

               
                rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

              
                if (Mathf.Abs(player.position.x - transform.position.x) < 0.2f)
                {
                   
                     direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y).normalized;

                     rb.velocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
                 }
                
            }
            else
            {
                
                rb.velocity = Vector2.zero;
            }
        }
    }
}