using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityEngine;

public class Climbing : MonoBehaviour
{
    public bool greenBox, redBox;
    public float redXOffset, redYOffSet, redXSize, redYSize, greenXOffset, greenYOffset, greenXsize, greenYSize;
    public Rigidbody2D rb;
    public float startingGrav;
    public LayerMask groundMask;
    private Vector2 previousVelocity;
    public Vector2 wallposition;

    // Start is called before the first frame update
=======
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

>>>>>>> b8982f2ecf1cb1adc76f9242e097dd6f13bd58c2
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingGrav = rb.gravityScale;
<<<<<<< HEAD
        previousVelocity = rb.velocity;
        
    }


    void Update()
    {


        UpdateBoxOffsets();

        greenBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (greenXOffset * transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXsize, greenYSize), 0f, groundMask);
        redBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (redXOffset * transform.localScale.x), transform.position.y + redYOffSet), new Vector2(redXSize, redYSize), 0f, groundMask);

        if (greenBox && !redBox && !playerVariables.isGrabbing)
        {
            playerVariables.isGrabbing = true;
=======
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
>>>>>>> b8982f2ecf1cb1adc76f9242e097dd6f13bd58c2
        }

        if (playerVariables.isGrabbing)
        {
<<<<<<< HEAD
            rb.velocity = new Vector2(0f, 0f);
            rb.gravityScale = 0f;
        }

      
        if (playerVariables.isGrabbing && Input.GetKeyDown(KeyCode.Space))
        {
            ChangePos();
        }

        
        if (playerVariables.isGrabbing && (!greenBox || !redBox))
        {
            ChangePos();
        }

        previousVelocity = rb.velocity; 
    }

    public void ChangePos()
    {

        Vector2 newPosition = new Vector2(transform.position.x + (0.5f * transform.localScale.x), transform.position.y + 1f);


        if (playerVariables.isGrabbing && Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = newPosition;
        }


        rb.gravityScale = startingGrav;
        playerVariables.isGrabbing = false;
    }

    void UpdateBoxOffsets()
    {

        if (previousVelocity.x > 0)
        {

            greenXOffset = Mathf.Abs(greenXOffset);
            redXOffset = Mathf.Abs(redXOffset);
        }

        else if (previousVelocity.x < 0)
        {

            greenXOffset = -Mathf.Abs(greenXOffset);
            redXOffset = -Mathf.Abs(redXOffset);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (redXOffset * transform.localScale.x), transform.position.y + redYOffSet), new Vector2(redXSize, redYSize));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (greenXOffset * transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXsize, greenYSize));
    }
=======
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
>>>>>>> b8982f2ecf1cb1adc76f9242e097dd6f13bd58c2
}
