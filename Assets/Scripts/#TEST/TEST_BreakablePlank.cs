using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_BreakablePlank : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string boxTag = "Box"; // Tag for detecting the box
    [SerializeField] private float fallThreshold = -2f; // Minimum falling speed to trigger

    private Collider2D objCollider;

    void Start()
    {
        objCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object that collided is a "Box"
        if (collision.gameObject.CompareTag(boxTag))
        {
            Rigidbody2D boxRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Check if the box is actually falling
            if (boxRb != null && boxRb.velocity.y < fallThreshold)
            {
                objCollider.enabled = false; // Disable collider
            }
        }
    }
}