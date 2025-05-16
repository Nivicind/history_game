using UnityEngine;

public class DebugVelocityLogger : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogWarning("No Rigidbody2D found on this GameObject.");
        }
    }

    void Update()
    {
        if (rb != null)
        {
            Debug.Log($"Velocity of {gameObject.name}: {rb.velocity}");
        }
    }
}
