using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public float jumpCooldown = 0.1f;
    public int maxJumps = 2;

    private bool isHurtCoroutineRunning;
    public bool isGrounded;
    private Rigidbody2D rb;
    private float lastJumpTime;
    private float lastAirJumpTime;
    public int jumpsPerformed;
    public float moveSpeed;

    public bool hurt;
    private bool mirrorX;
    private  SpriteRenderer spriteRenderer;

    public GameObject Effector;

    private Animator AnimatorPlayer;

    public bool isTouchingWall;
    public LayerMask wallLayer;
    public float horizontalInput;
    void Start()
    {
        AnimatorPlayer = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(hurt){
            HurtPlayer();
        }

        Input.GetAxis("Horizontal");
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
        else if(Input.GetKeyDown(KeyCode.Space) && (!isGrounded) && (!isTouchingWall) && Time.time - lastAirJumpTime > jumpCooldown){
            if(jumpsPerformed<1){
                AirJump();
            }
                  
        }
         // Check if the player is touching a wall
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right  * (spriteRenderer.flipX ? -1 : 1) , 0.6f, wallLayer);

        // Wall Jump
        if (isTouchingWall && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            WallJump();
        }


        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Move(horizontalInput);

         if(isGrounded ){
            if(horizontalInput != 0){
                AnimatorPlayer.Play("WalkingPlayer");   
            }
            else{
                AnimatorPlayer.Play("IdlePlayer");   
            }
        }else{
            if(isTouchingWall){
                    AnimatorPlayer.Play("WallSlidePlayer"); 

            }else{
                if(rb.velocityY < 0){
                    AnimatorPlayer.Play("FallingPlayer"); 

                }
                if(rb.velocityY > 0){
                    AnimatorPlayer.Play("JumpingPlayer"); 
                }

            }
        }

    }


    void Move(float direction)
    {
        float horizontalVelocity = direction * moveSpeed;
        if(isGrounded){
            
            rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
            

        }
        else{
            rb.velocity = new Vector2(rb.velocityX, rb.velocity.y);
            
        }
        
       
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

    void WallJump(){

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if(!mirrorX){
            rb.AddRelativeForceX(-5,ForceMode2D.Impulse);
        }else{
            rb.AddRelativeForceX(5,ForceMode2D.Impulse);
        }
            
   
        lastAirJumpTime = Time.time;
        //jumpsPerformed++;
        GameObject effector = Instantiate(Effector, transform.position, Quaternion.identity);
        effector.GetComponent<Effector>().selectedAnimation = "AirJumpPlayer"; //EffectorNothing
        if (!mirrorX)
        {
            effector.GetComponent<SpriteRenderer>().flipX = true;
        }else if(mirrorX) {
             effector.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void HurtPlayer(){
        AnimatorPlayer.Play("HurtPlayer"); 

        rb.velocity = Vector2.zero; // to not sum the velocity before
        rb.AddForceY(3,ForceMode2D.Impulse);
        if(!mirrorX){
            rb.AddRelativeForceX(-3,ForceMode2D.Impulse);
        }else{
            rb.AddRelativeForceX(3,ForceMode2D.Impulse);
        }
        hurt = false;

    }

}