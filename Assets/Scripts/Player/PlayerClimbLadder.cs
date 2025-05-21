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
        UpdateLadderVisual();
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
        LadderState state = ladderCollider.GetComponentInParent<LadderState>();
        bool isFacingRight = transform.localScale.x > 0;
        
        if (state != null)
            {
                // ✅ Compare ladder's facing direction with player's facing direction
                if (state.facingRightLadder == isFacingRight)
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

    void UpdateLadderVisual()
    {
        // Find all ladders
        GameObject[] allLadders = GameObject.FindGameObjectsWithTag("Ladder");

        foreach (GameObject ladder in allLadders)
        {
            LadderState state = ladder.GetComponentInParent<LadderState>();
            if (state == null) continue;

            // Active: player is climbing this ladder
            if (IsClimbLadder && currentLadder == ladder)
            {
                state.SetLadderState(LadderState.LadderVisualState.Active);
            }
            // Hover: near this ladder but not climbing
            else if (!IsClimbLadder && currentLadder == ladder)
            {
                state.SetLadderState(LadderState.LadderVisualState.Hover);
            }
            // Idle: all others
            else
            {
                state.SetLadderState(LadderState.LadderVisualState.Idle);
            }
        }
    }

    void ClimbLadderAnimationHadler()
    {
        animator.SetBool("isClimbLadder", IsClimbLadder);
        animator.SetFloat("verticalInput", vertical);
    }
}