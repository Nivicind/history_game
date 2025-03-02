using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Handles player climbing mechanics using detection boxes.
/// The ledge check flips based on player direction, so only one set is needed.
/// </summary>
public class Climbing : MonoBehaviour
{
    [Header("Ledge Detection")]
    public Transform LedgeCheckBelow;  // Detects if there is ground below
    public Transform LedgeCheckAbove;  // Detects if the ledge is climbable
    public Vector2 LedgeBelowSize;
    public Vector2 LedgeAboveSize;

    [Header("Player Components")]
    public Rigidbody2D rb;
    private float startingGrav;

    [Header("Climbing Settings")]
    public LayerMask groundMask;
    private bool isClimbing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingGrav = rb.gravityScale;
    }

    void Update()
    {
        bool belowCheck = Physics2D.OverlapBox(LedgeCheckBelow.position, LedgeBelowSize, 0f, groundMask);
        bool aboveCheck = Physics2D.OverlapBox(LedgeCheckAbove.position, LedgeAboveSize, 0f, groundMask);

        // Start climbing if below check detects ground and above check is empty
        if (belowCheck && !aboveCheck && !playerVariables.isGrabbing)
        {
            playerVariables.isGrabbing = true;
            isClimbing = true;
        }

        if (playerVariables.isGrabbing)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }

        // Climb when pressing Space
        if (playerVariables.isGrabbing && Input.GetKeyDown(KeyCode.Space))
        {
            Climb();
        }

        // Stop grabbing if no longer in a valid position
        if (playerVariables.isGrabbing && !belowCheck)
        {
            Climb();
        }
    }

    /// <summary>
    /// Moves the player up and restores gravity.
    /// </summary>
    public void Climb()
    {
        transform.position = new Vector2(transform.position.x + (0.52f * transform.localScale.x), transform.position.y + 0.6f);
        rb.gravityScale = startingGrav;
        playerVariables.isGrabbing = false;
        isClimbing = false;
    }

    /// <summary>
    /// Draws Gizmos for visualization in the Unity Editor.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (LedgeCheckBelow != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(LedgeCheckBelow.position, LedgeBelowSize);
        }

        if (LedgeCheckAbove != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(LedgeCheckAbove.position, LedgeAboveSize);
        }
    }
}
