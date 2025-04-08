using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public CapsuleCollider2D playerCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite standingSprite, crouchingSprite;
    public Animator animator;

    public Vector2 crouchingSize;
    public Vector2 crouchingOffset;

    private Vector2 standingSize;
    private Vector2 standingOffset;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isCrouching;
    private bool isMoving;
    private bool isJumping;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        standingSize = playerCollider.size;
        standingOffset = playerCollider.offset;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGrounded();
        Move();
        Jump();
        Crouch();
    }
    
    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float moveSpeed = isCrouching ? crouchSpeed : speed;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        
        isMoving = moveInput != 0;
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        // if (moveInput != 0)
        // {
        //     animator.SetBool("isRunning", true);
        // }
        // else
        // {
        //     animator.SetBool("isRunning", true);
        // }
    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
            
        }
        else if (isGrounded)
        {
            isJumping = false;
        }
    }
    
    void Crouch()
    {
        if (Input.GetKey(KeyCode.C) && isGrounded)
        {
            isCrouching = true;
            playerCollider.size = crouchingSize;
            playerCollider.offset = crouchingOffset;
            spriteRenderer.sprite = crouchingSprite;
        }
        else
        {
            isCrouching = false;
            playerCollider.size = standingSize;
            playerCollider.offset = standingOffset;
            spriteRenderer.sprite = standingSprite;
        }
    }




    private void OnTriggerEnter2D(Collider2D collision){
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }
}
