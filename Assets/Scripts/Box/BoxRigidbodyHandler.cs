using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoxRigidbodyHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerPushBoxes playerPushBoxes;

    [Header("Box Ground Check")]
    public Transform boxGroundCheck;
    public LayerMask groundLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPushBoxes = FindObjectOfType<PlayerPushBoxes>();
    }

    void Update()
    {
        UpdateRigidbodyState();
    }

    public void UpdateRigidbodyState()
    {

    }

    public bool IsBoxGrounded()
    {
        if (boxGroundCheck == null) return false;

        Collider2D boxGroundCollider = boxGroundCheck.GetComponent<Collider2D>();
        return boxGroundCollider != null && boxGroundCollider.IsTouchingLayers(groundLayer);
    }
}