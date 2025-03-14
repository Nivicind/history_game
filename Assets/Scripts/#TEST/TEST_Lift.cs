using UnityEngine;

public class TEST_Lift : MonoBehaviour
{
    [Header("Lift Settings")]
    [SerializeField] private GameObject lift1;  // First lift
    [SerializeField] private GameObject lift2;  // Second lift (moves opposite)
    [SerializeField] private float moveSpeed = 3f;

    [Header("Lift 1 Limits")]
    [SerializeField] private float lift1MinY = 0f;
    [SerializeField] private float lift1MaxY = 5f;

    [Header("Lift 2 Limits")]
    [SerializeField] private float lift2MinY = 0f;
    [SerializeField] private float lift2MaxY = 5f;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange)
        {
            float newY1 = lift1.transform.position.y;
            float newY2 = lift2.transform.position.y;

            if (Input.GetKey(KeyCode.Q)) // Move Lift 1 UP & Lift 2 DOWN
            {
                newY1 += moveSpeed * Time.deltaTime;
                newY2 -= moveSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.E)) // Move Lift 1 DOWN & Lift 2 UP
            {
                newY1 -= moveSpeed * Time.deltaTime;
                newY2 += moveSpeed * Time.deltaTime;
            }

            // Clamp values to prevent moving out of bounds
            newY1 = Mathf.Clamp(newY1, lift1MinY, lift1MaxY);
            newY2 = Mathf.Clamp(newY2, lift2MinY, lift2MaxY);

            // Apply movement
            lift1.transform.position = new Vector2(lift1.transform.position.x, newY1);
            lift2.transform.position = new Vector2(lift2.transform.position.x, newY2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
