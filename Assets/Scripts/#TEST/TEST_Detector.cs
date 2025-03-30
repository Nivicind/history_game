using UnityEngine;

public class TEST_Detector : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 5f;
    private Vector3 startPos;
    private int direction = 1;

    private bool playerIsHidden = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Move left and right
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        // Reverse direction when reaching limits
        if (Mathf.Abs(transform.position.x - startPos.x) > moveDistance)
        {
            direction *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerIsHidden)
        {
            Debug.Log("Player Detected!");
        }
    }

    public void SetPlayerHidden(bool isHidden)
    {
        playerIsHidden = isHidden;
    }
}
