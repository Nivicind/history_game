using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    public bool greenBox, redBox;
    public float redXOffset, redYOffSet, redXSize, redYSize, greenXOffset, greenYOffset, greenXsize, greenYSize;
    public Rigidbody2D rb;
    public float startingGrav;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingGrav = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
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

        // Gọi ChangePos khi người chơi nhấn Space và đang bám
        if (playerVariables.isGrabbing && Input.GetKeyDown(KeyCode.Space))
        {
            ChangePos();
        }

        // Tự động ngừng bám nếu không còn ở vị trí hợp lệ
        if (playerVariables.isGrabbing && (!greenBox || !redBox))
        {
            ChangePos();
        }
    }

    public void ChangePos()
    {
        transform.position = new Vector2(transform.position.x + (0.52f * transform.localScale.x), transform.position.y + 0.6f);
        rb.gravityScale = startingGrav;
        playerVariables.isGrabbing = false;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (redXOffset * transform.localScale.x), transform.position.y + redYOffSet), new Vector2(redXSize, redYSize));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (greenXOffset * transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXsize, greenYSize));
    }
}