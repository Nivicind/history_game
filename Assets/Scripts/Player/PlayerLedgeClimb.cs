using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimb : MonoBehaviour
{
    [Header("Climbing Detection - Green Box")]
    [Tooltip("Detects climbable surfaces at the upper part of the character.")]
    public bool greenBox;
    public float greenXOffset;
    public float greenYOffset;
    public float greenXSize;
    public float greenYSize;

    [Header("Climbing Detection - Red Box")]
    [Tooltip("Detects surfaces below the green box to ensure a valid climb.")]
    public bool redBox;
    public float redXOffset;
    public float redYOffset;
    public float redXSize;
    public float redYSize;

    [Header("Player Physics & Components")]
    [Tooltip("Reference to the player's Rigidbody2D component.")]
    public Rigidbody2D rb;
    [Tooltip("Stores the player's default gravity scale.")]
    public float startingGrav;
    [Tooltip("Defines which layers are considered ground.")]
    public LayerMask groundMask;
    private Vector2 previousVelocity;



    public Vector2 storeposition;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingGrav = rb.gravityScale;
        previousVelocity = rb.velocity;
    }

    void Update()
    {
        UpdateBoxOffsets();

        greenBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (greenXOffset * transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXSize, greenYSize), 0f, groundMask);
        redBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (redXOffset * transform.localScale.x), transform.position.y + redYOffset), new Vector2(redXSize, redYSize), 0f, groundMask);

        if (greenBox && !redBox && !PlayerVariables.isGrabbing)
        {
            PlayerVariables.isGrabbing = true;
        }

        if (PlayerVariables.isGrabbing)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }

        if (PlayerVariables.isGrabbing && Input.GetKeyDown(KeyCode.Space))
        {
            ChangePos();
        }

        // Kiểm tra nếu không còn bám vào tường nữa thì bỏ trạng thái bám
        if (!greenBox && !redBox)
        {
            PlayerVariables.isGrabbing = false;
            rb.gravityScale = startingGrav;
        }

        previousVelocity = rb.velocity;
    }


    public void ChangePos()
    {
        if (!PlayerVariables.isGrabbing)
            return;

        Vector2 newPosition = new Vector2(transform.position.x + (0.5f * transform.localScale.x), transform.position.y + 1f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = newPosition;
            rb.gravityScale = startingGrav;
            PlayerVariables.isGrabbing = false;
        }
        else
        {
            rb.velocity = Vector2.zero; 
            rb.gravityScale = 0f;
        }
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
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (redXOffset * transform.localScale.x), transform.position.y + redYOffset), new Vector2(redXSize, redYSize));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (greenXOffset * transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXSize, greenYSize));
    }
}
