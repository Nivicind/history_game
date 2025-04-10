using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Player Collider")]
    public CapsuleCollider2D playerCollider;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite standingSprite, crouchingSprite;

    [Header("Animator")]
    public Animator animator;

    [Header("Crouching Settings")]
    public Vector2 crouchingOffset;
    public Vector2 crouchingSize;

    private Vector2 standingOffset;
    private Vector2 standingSize;

    private Rigidbody2D rb;
    private PlayerPushBoxes boxHandler;

    private bool isGrounded;
    private bool isCrouching = false;
    private bool isMoving;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        standingSize = playerCollider.size;
        standingOffset = playerCollider.offset;
        animator = GetComponent<Animator>();
        boxHandler = GetComponent<PlayerPushBoxes>();
    }

    void Update()
    {
        CheckGrounded();
        Move();
        Jump();
        Crouch();
        animator.SetBool("isJumping", !isGrounded);
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

        if (moveInput != 0)
        {
            animator.SetBool("isRunning", true);
            Flip(moveInput);
        }

        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    void Flip(float moveInput)
    {
        // Prevent flipping if the player is attached to a box
        if (boxHandler != null && boxHandler.isAttached)
            return;

        if (moveInput < 0)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else if (moveInput > 0)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching && !boxHandler.isDraggingBox)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
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