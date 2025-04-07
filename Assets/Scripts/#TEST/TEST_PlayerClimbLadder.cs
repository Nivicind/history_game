using UnityEngine;

public class TEST_PlayerClimbLadder : MonoBehaviour
{
    [Header("Ladder Climbing Settings")]
    [SerializeField] private float climbSpeed = 8f;
    [SerializeField] private float normalGravity = 2f;
    [SerializeField] private Rigidbody2D rb;

    private float vertical;
    private bool isLadder;
    private bool isClimbing;

    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        // Start climbing if on a ladder and pressing up/down
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
        // Stop climbing if player is idle
        else if (isClimbing && vertical == 0f)
        {
            isClimbing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f; // Disable gravity while climbing
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
        else
        {
            rb.gravityScale = normalGravity; // Restore gravity when not climbing
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false; // Stop climbing when leaving ladder
        }
    }
}
