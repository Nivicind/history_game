using UnityEngine;

public class BoxBreakingPlank : MonoBehaviour
{
    [SerializeField] private float breakVelocityThreshold = -10f; // Threshold to break plank

    private Rigidbody2D rb;
    private float lastFallingVelocityY = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Track the last falling velocity (only if moving downward)
        if (rb.velocity.y < 0)
        {
            lastFallingVelocityY = rb.velocity.y;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plank"))
        {
            // Use the cached falling speed instead of current velocity
            if (lastFallingVelocityY < breakVelocityThreshold)
            {
                BreakablePlank plank = collision.gameObject.GetComponent<BreakablePlank>();
                if (plank != null)
                {
                    plank.Break();
                }
            }

            // Reset the stored velocity after the hit (optional)
            lastFallingVelocityY = 0f;
        }
    }
}
