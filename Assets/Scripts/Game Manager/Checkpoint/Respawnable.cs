using UnityEngine;

public class Respawnable : MonoBehaviour
{
    private Vector2 initialPos;
    private Quaternion initialRot;
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D objCollider;
    private bool wasActive;
    private bool isBreakablePlank = false;
    private bool wasBrokenAtCheckpoint = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        objCollider = GetComponent<Collider2D>();

        // Detect if this object is a BreakablePlank
        isBreakablePlank = GetComponent<BreakablePlank>() != null;

        CheckpointManager.Instance.RegisterRespawnable(this);
        SaveState();
    }

    public void SaveState()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
        wasActive = gameObject.activeSelf;

        if (isBreakablePlank)
        {
            var plank = GetComponent<BreakablePlank>();
            wasBrokenAtCheckpoint = plank.IsBroken();
        }
    }

    public void RestoreState()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
        gameObject.SetActive(wasActive);

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (isBreakablePlank)
        {
            var plank = GetComponent<BreakablePlank>();
            if (wasBrokenAtCheckpoint)
            {
                plank.BreakInstant(); // You need to add this method
            }
            else
            {
                plank.ResetPlank();  // You need to add this method
            }
        }
    }
}
