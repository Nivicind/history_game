using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D RB;

    [Header("Movement Variables")]
    public float moveSpeed = 5f;
    private Vector2 _moveInput;
    private bool _isFacingRight = true;

    [Header("Jump Variables")]
    public float jumpForce = 10f;

    [Header("Ground and Wall Check")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [SerializeField] private LayerMask _groundLayer;
    
    [Header("Coyote Time")]
    public float _coyoteTime = 0.1f;
    private float _coyoteTimeCounter;

    [Header("Jump Buffering")]
    public float _jumpBufferTime = 0.2f; // How long the jump input is stored
    private float _jumpBufferCounter; // Timer for jump buffering

    [Header("Ledge Grab")]
    [SerializeField] private Transform _frontLedgeGrab;
    [SerializeField] private Transform _backLedgeGrab;
    [SerializeField] private Vector2 _ledgeCheckSize = new Vector2(0.5f, 0.5f);
    private bool _isGrabbingLedge = false;
    private Vector2 _ledgePosition;

    [Header("State Variables")]
    private bool _isMoving;
    private bool _isJumping;
    private bool _isFalling;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get movement input (-1 for left, 1 for right)
        _moveInput.x = Input.GetAxisRaw("Horizontal");

        // Update movement state
        _isMoving = _moveInput.x != 0;

        // Flip character if moving in a different direction
        if (_isMoving)
            CheckDirectionToFace(_moveInput.x > 0);

        // Ground check using OverlapBox
        bool isGrounded = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);

        // Coyote time logic
        if (isGrounded)
        {
            _coyoteTimeCounter = _coyoteTime; // Reset coyote time when grounded
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump Buffering logic
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime; // Store jump input for a short duration
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        // Jump execution (only if jump buffer or coyote time is active)
        if (_jumpBufferCounter > 0 && _coyoteTimeCounter > 0)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
            _isJumping = true;
            _isFalling = false;
            _jumpBufferCounter = 0; // Reset jump buffer after jumping
        }

        // Update falling state
        if (RB.velocity.y < 0)
        {
            _isFalling = true;
            _isJumping = false;
        }
        else if (RB.velocity.y > 0)
        {
            _isFalling = false;
        }

        // Reset jump state when grounded
        if (isGrounded)
        {
            _isJumping = false;
            _isFalling = false;
        }

        // Debug logs for player state
        // if (isGrounded && !_isMoving && !_isJumping && !_isFalling)
        // {
        //     Debug.Log("Player is standing.");
        // }
        // else if (_isMoving && !_isJumping && !_isFalling)
        // {
        //     Debug.Log("Player is moving.");
        // }
        // else if (_isJumping)
        // {
        //     Debug.Log("Player is jumping.");
        // }
        // else if (_isFalling)
        // {
        //     Debug.Log("Player is falling.");
        // }
    }

    void FixedUpdate()
    {
        RB.velocity = new Vector2(_moveInput.x * moveSpeed, RB.velocity.y);
    }

    private void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != _isFacingRight)
            Flip();
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
