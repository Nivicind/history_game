using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D RB;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private Vector2 _moveInput;
    private bool _isFacingRight = true;

    [Header("Jump Settings")]
    public float jumpForce = 10f;

    [Header("Ground & Wall Detection")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;
    //public bool checkladder = true;

    [Header("Coyote Time")]
    public float coyoteTime = 0.1f;
    private float _coyoteTimeCounter;

    [Header("Jump Buffer")]
    public float jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;

    [Header("State Variables")]
    private bool _isJumping;
    private bool _isFalling;

    public bool ClimbingAllowed { get; set; }
    private float dirX, dirY;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }


    float slowdown(float moveSpeed)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 3f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5f;
        }
        return moveSpeed;
    }

    void Update()
    {
        moveSpeed =  slowdown(moveSpeed);
        LadderClimbing();
        HandleInput();
        HandleJumpBuffering();
        HandleCoyoteTime();
        HandleJumpExecution();
        HandleFallingState();
        
        

    }


    void FixedUpdate()
    {
        MovePlayer();
        if (ClimbingAllowed)
        {
            RB.isKinematic = true;
            RB.velocity = new Vector2(dirX, dirY);
        }
        else
        {
            RB.isKinematic = false;
            RB.velocity = new Vector2(dirX, RB.velocity.y);
        }
    }

    public void LadderClimbing()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;




        if (ClimbingAllowed)
        {

            if (CheckGrounded(_isGrounded) && Input.GetAxisRaw("Vertical") < 0)
            {
                dirY = 0;
            }

            else
            {
                dirY = Input.GetAxisRaw("Vertical") * moveSpeed;
            }
        }

    }


    private void HandleJumpExecution()
    {
        // Jump only if jump buffer and coyote time are active, or if grounded and near ladder
        if (_jumpBufferCounter > 0 && (_coyoteTimeCounter > 0 || (_isGrounded && ClimbingAllowed)))
        {
            Jump();
            _jumpBufferCounter = 0; // Reset jump buffer after jumping
            if (ClimbingAllowed)
            {
                ClimbingAllowed = false; // Tạm thời thoát trạng thái leo thang khi nhảy
                RB.isKinematic = false;  // Khôi phục Rigidbody2D về trạng thái bình thường
            }
        }
    }

    private bool CheckGrounded(bool isGrounded)
    {
        isGrounded = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);
        return isGrounded;
    }



    private void HandleInput()
    {
        // Get movement input (-1 for left, 1 for right)
        _moveInput.x = Input.GetAxisRaw("Horizontal");

        // Flip character if moving in a different direction
        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);
    }

    private void HandleJumpBuffering()
    {
        // Store jump input for a short duration
        if (Input.GetButtonDown("Jump"))
            _jumpBufferCounter = jumpBufferTime;
        else
            _jumpBufferCounter -= Time.deltaTime;
    }

    private void HandleCoyoteTime()
    {
        // Ground check
        _isGrounded = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);

        // Reset coyote time when grounded
        if (_isGrounded)
            _coyoteTimeCounter = coyoteTime;
        else
            _coyoteTimeCounter -= Time.deltaTime;
    }

    

    private void HandleFallingState()
    {
        if (RB.velocity.y < -0.1f) // Slight threshold to prevent premature falling state
        {
            _isFalling = true;
            _isJumping = false;
            PlayerVariables.isJumping = false;

        }
        else if (RB.velocity.y > 0.1f)
        {
            _isFalling = false;
            _isJumping = true;
            PlayerVariables.isJumping = true;

        }
        else
        {
            _isFalling = false;
            _isJumping = false;

        }
    }

    private void MovePlayer()
    {
        RB.velocity = new Vector2(_moveInput.x * moveSpeed, RB.velocity.y);
    }

    private void Jump()
    {
        RB.velocity = new Vector2(RB.velocity.x, jumpForce);
        _isJumping = true;
        _coyoteTimeCounter = 0; // Reset coyote time after jumping
    }

    private void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != _isFacingRight)
            Flip();
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f); // Flips only on the Y-axis
    }

    void OnDrawGizmos()
    {
        if (_groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
        }
    }
}