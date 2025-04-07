using UnityEngine;

public class TEST_Lift : MonoBehaviour
{
    [Header("Lift Settings")]
    [SerializeField] private GameObject lift;  // Assign the lift object here
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float minY = 0f;  // Lowest world coordinate
    [SerializeField] private float maxY = 5f;  // Highest world coordinate

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange)
        {
            float newY = lift.transform.position.y;

            if (Input.GetKey(KeyCode.Q) && newY < maxY)
            {
                newY += moveSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.E) && newY > minY)
            {
                newY -= moveSpeed * Time.deltaTime;
            }

            // Clamp to make sure it doesn't go beyond limits
            newY = Mathf.Clamp(newY, minY, maxY);

            // Apply movement
            lift.transform.position = new Vector2(lift.transform.position.x, newY);
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
