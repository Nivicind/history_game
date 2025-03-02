using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingGrav = rb.gravityScale;
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
        }

        if (playerVariables.isGrabbing)
        {
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
}
