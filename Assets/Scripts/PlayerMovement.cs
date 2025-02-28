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

    [Header("Coyote Time")]
    public float coyoteTime = 0.1f;
    private float _coyoteTimeCounter;

    [Header("Jump Buffer")]
    public float jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;

    [Header("State Variables")]
    private bool _isJumping;
    private bool _isFalling;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        HandleJumpBuffering();
        HandleCoyoteTime();
        HandleJumpExecution();
        HandleFallingState();
    }

    void FixedUpdate()
    {
        MovePlayer();
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

    private void HandleJumpExecution()
    {
        // Jump only if jump buffer and coyote time are active
        if (_jumpBufferCounter > 0 && _coyoteTimeCounter > 0)
        {
            Jump();
            _jumpBufferCounter = 0; // Reset jump buffer after jumping
        }
    }

    private void HandleFallingState()
    {
        if (RB.velocity.y < -0.1f) // Slight threshold to prevent premature falling state
        {
            _isFalling = true;
            _isJumping = false;
        }
        else if (RB.velocity.y > 0.1f)
        {
            _isFalling = false;
            _isJumping = true;
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
