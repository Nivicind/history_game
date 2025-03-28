using UnityEngine;

public class TEST_PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public CapsuleCollider2D playerCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite standingSprite, crouchingSprite;

    public Vector2 crouchingSize; // Now public, adjustable in Unity
    public Vector2 crouchingOffset; // Now public, adjustable in Unity

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
    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
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
}
