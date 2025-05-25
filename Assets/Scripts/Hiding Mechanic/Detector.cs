using UnityEngine;

public class TEST_Detector : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Collider2D movementBounds; // Assign your confiner object here
    public int startDirection = 1;
    private float minX;
    private float maxX;

    void Start()
    {
        if (movementBounds == null)
        {
            Debug.LogError("Movement Bounds collider is not assigned!");
            return;
        }

        // Calculate min and max X from bounds
        Bounds bounds = movementBounds.bounds;
        minX = bounds.min.x;
        maxX = bounds.max.x;
    }

    void Update()
    {
        if (movementBounds == null) return;

        // Move the detector
        transform.position += Vector3.right * startDirection * moveSpeed * Time.deltaTime;

        // Bounce off the bounds
        if (transform.position.x > maxX)
        {
            startDirection = -1;
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < minX)
        {
            startDirection = 1;
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
    }
}
