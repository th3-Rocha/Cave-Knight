using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public float jumpCooldown = 0.1f;
    public int maxJumps = 2;

    public bool isGrounded;
    private Rigidbody2D rb;
    private float lastJumpTime;
    private float lastAirJumpTime;
    public int jumpsPerformed;
    public float moveSpeed;

    private bool mirrorX;
    private  SpriteRenderer spriteRenderer;

    public GameObject Effector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jumps if grounded
        if (isGrounded)
        {
            jumpsPerformed = 0;
        }

        // Jump 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && Time.time - lastJumpTime > jumpCooldown)
        {
            Jump();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && (!isGrounded) && Time.time - lastAirJumpTime > jumpCooldown){
            if(jumpsPerformed<1){
                AirJump();
                

            }
                  
        }


        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Move(horizontalInput);
    }


    void Move(float direction)
    {
        float horizontalVelocity = direction * moveSpeed; // Add a moveSpeed variable to control the movement speed
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
      
        if(direction>0){
            mirrorX = false;
        }else if (direction<0){
            mirrorX = true;

        }
         spriteRenderer.flipX = mirrorX;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        lastJumpTime = Time.time;
       
        GameObject effector = Instantiate(Effector, transform.position, Quaternion.identity);
        effector.GetComponent<Effector>().selectedAnimation = "GroundJumpPlayer"; //EffectorNothing
        if (!mirrorX)
        {
            effector.GetComponent<SpriteRenderer>().flipX = true;
        }else if(mirrorX) {
             effector.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void AirJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        lastAirJumpTime = Time.time;
        jumpsPerformed++;
        GameObject effector = Instantiate(Effector, transform.position, Quaternion.identity);
        effector.GetComponent<Effector>().selectedAnimation = "AirJumpPlayer"; //EffectorNothing
        if (!mirrorX)
        {
            effector.GetComponent<SpriteRenderer>().flipX = true;
        }else if(mirrorX) {
             effector.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}