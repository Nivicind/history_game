using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoxRigidbodyHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerPushBoxes playerPushBoxes;

    [Header("Box Ground Check")]
    public Transform boxGroundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPushBoxes = FindObjectOfType<PlayerPushBoxes>();
    }

    public bool IsBoxGrounded()
    {
        if (boxGroundCheck == null) return false;

        Collider2D boxGroundCollider = boxGroundCheck.GetComponent<Collider2D>();
        return boxGroundCollider != null && boxGroundCollider.IsTouchingLayers(groundLayer);
    }

    public bool IsBoxOnTopOfAnotherBox()
    {
        if (boxGroundCheck == null) return false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(boxGroundCheck.position, groundCheckRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Box Top") && hit.gameObject != this.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (boxGroundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(boxGroundCheck.position, groundCheckRadius);
        }
    }
}
