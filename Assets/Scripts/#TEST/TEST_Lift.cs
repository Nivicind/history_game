using UnityEngine;

public class TEST_Lift : MonoBehaviour
{
    [Header("Lift Settings")]
    [SerializeField] private GameObject lift;             // Reference to the lift GameObject
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private LayerMask obstacleLayers;    // Layers that the lift should not go through
    [SerializeField] private float detectionDistance = 0.05f;

    private BoxCollider2D liftCollider;
    private bool playerInRange = false;

    void Start()
    {
        liftCollider = lift.GetComponent<BoxCollider2D>();
        if (liftCollider == null)
            Debug.LogWarning("Lift object has no BoxCollider2D!");
    }

    void Update()
    {
        if (!playerInRange || liftCollider == null) return;

        Vector2 currentPos = lift.transform.position;
        float targetY = currentPos.y;

        // Move Up
        if (Input.GetKey(KeyCode.Q) && currentPos.y < maxY)
        {
            targetY += moveSpeed * Time.deltaTime;
        }
        // Move Down
        else if (Input.GetKey(KeyCode.E) && currentPos.y > minY)
        {
            // Cast a box below the lift to detect any blocking objects
            Vector2 boxSize = liftCollider.bounds.size;
            Vector2 boxCenter = (Vector2)lift.transform.position + Vector2.down * (boxSize.y / 2f + detectionDistance / 2f);
            RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, detectionDistance, obstacleLayers);

            if (hit.collider == null)
            {
                targetY -= moveSpeed * Time.deltaTime;
            }
            else
            {
                Debug.Log("Obstacle below: " + hit.collider.name);
            }
        }

        targetY = Mathf.Clamp(targetY, minY, maxY);
        lift.transform.position = new Vector2(currentPos.x, targetY);
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
