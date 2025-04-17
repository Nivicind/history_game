using UnityEngine;

public class PlayerClimbLadder : MonoBehaviour
{
    [Header("Ladder Climbing Settings")]
    [SerializeField] private float climbSpeed = 8f;
    [SerializeField] private float normalGravity = 2f;
    [SerializeField] private Rigidbody2D rb;

    [Header("Ladder Check Settings")]
    [SerializeField] private Transform ladderCheck;
    [SerializeField] private float checkRadius = 0.5f;
    [SerializeField] private LayerMask ladderLayer;

    [Header("Animation Settings")]
    public Animator animator;

    [Header("State Variables")]
    private float vertical;
    private bool isNearLadder;
    public bool IsClimbLadder;

    private GameObject currentLadder;
    private PlayerPushBoxes boxHandler;

    void Start()
    {
        boxHandler = GetComponent<PlayerPushBoxes>();
    }

    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        CheckLadderProximity();
        HandleLadderClimbing();
        ClimbLadderAnimationHadler();
    }

    private void FixedUpdate()
    {
        if (IsClimbLadder)
        {
            rb.gravityScale = 0f; // Disable gravity while climbing
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
        else
        {
            rb.gravityScale = normalGravity; // Restore gravity when not climbing
        }
    }

    void CheckLadderProximity()
    {
        Collider2D ladderCollider = Physics2D.OverlapCircle(ladderCheck.position, checkRadius, ladderLayer);

        if (ladderCollider != null)
        {
            // Check if the ladder's tag matches the player's facing direction
            bool isFacingRight = transform.localScale.x > 0;
            if ((isFacingRight && ladderCollider.CompareTag("Right Ladder")) ||
                (!isFacingRight && ladderCollider.CompareTag("Left Ladder")))
            {
                currentLadder = ladderCollider.gameObject;
                isNearLadder = true;
            }
            else
            {
                currentLadder = null;
                isNearLadder = false;
            }
        }
        else
        {
            currentLadder = null;
            isNearLadder = false;
        }
    }

    void HandleLadderClimbing()
    {
        // Prevent climbing if dragging a box
        if (boxHandler != null && boxHandler.isDraggingBox)
        {
            StopClimbing();
            return;
        }

        if (isNearLadder && Mathf.Abs(vertical) > 0f && !IsClimbLadder)
        {
            StartClimbing();
        }

        if (!isNearLadder && IsClimbLadder)
        {
            StopClimbing();
        }
    }

    void StartClimbing()
    {
        IsClimbLadder = true;
        rb.velocity = Vector2.zero;
    }

    void StopClimbing()
    {
        IsClimbLadder = false;
    }

    void ClimbLadderAnimationHadler()
    {
        animator.SetBool("isClimbLadder", IsClimbLadder);
    }
}