using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushBoxes : MonoBehaviour
{
    [Header("References")]
    public Transform pushPullCheck;
    public LayerMask boxLayer;
    public Animator animator;

    [Header("State Variables")]
    public bool IsAttachedToBox = false;
    public bool isDraggingBox { get; private set; } = false;
    public bool playerNotGrounded { get; private set; } = false;
    public GameObject currentBox;
    private PlayerMovement playerMovement;
    private FixedJoint2D joint;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!IsAttachedToBox)
        {
            CheckForBox();
        }

        AutoDetach();
        UpdateBoxVisual();
        HandleAttachOrDetach();
        PushPullAnimationHandler();
    }

    void AutoDetach()
    {
        playerNotGrounded = !playerMovement.isGrounded;

        if (IsAttachedToBox && currentBox != null)
        {
            BoxRigidbodyHandler boxRigidbodyHandler = currentBox.GetComponent<BoxRigidbodyHandler>();
            if (boxRigidbodyHandler != null && (playerNotGrounded || !boxRigidbodyHandler.IsBoxGrounded()))
            {
                DetachBox();
            }
        }
    }

    void CheckForBox()
    {
        Collider2D boxCollider = Physics2D.OverlapCircle(pushPullCheck.position, 0.1f, boxLayer);

        if (boxCollider != null)
        {
            currentBox = boxCollider.gameObject;
        }
        else
        {
            currentBox = null;
        }
    }

    void HandleAttachOrDetach()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!IsAttachedToBox && currentBox != null)
            {
                AttachBox();
            }
            else if (IsAttachedToBox)
            {
                DetachBox();
            }
        }
    }

    void AttachBox()
    {
        
        Rigidbody2D boxRb = currentBox.GetComponent<Rigidbody2D>();

        boxRb.mass = 10f;

        joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = boxRb;

        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector2.zero;

        Vector2 connectedAnchor = boxRb.transform.InverseTransformPoint(transform.position);
        joint.connectedAnchor = connectedAnchor;

        IsAttachedToBox = true;
        isDraggingBox = true;
    }

    void DetachBox()
    {
        if (joint != null)
        {
            Destroy(joint);
        }

        Rigidbody2D boxRb = currentBox.GetComponent<Rigidbody2D>();
        boxRb.mass = 100.0f;
        
        currentBox = null;
        IsAttachedToBox = false;
        isDraggingBox = false;
    }

    void UpdateBoxVisual()
    {
        GameObject[] allBoxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject box in allBoxes)
        {
            BoxState boxState = box.GetComponent<BoxState>();
            if (boxState == null) continue;

            if (IsAttachedToBox && currentBox == box)
            {
                boxState.SetState(BoxVisualState.Dragged);
            }
            else if (!IsAttachedToBox && currentBox == box)
            {
                boxState.SetState(BoxVisualState.Hover);
            }
            else
            {
                boxState.SetState(BoxVisualState.Idle);
            }
        }
    }

    void PushPullAnimationHandler()
    {
        if (animator == null || playerMovement == null) return;

        animator.SetBool("isPushing", IsAttachedToBox);

        if (IsAttachedToBox)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(horizontalInput) > 0.01f)
            {
                animator.speed = 1f;

                bool facingRight = playerMovement.isFacingRight;
                bool movingRight = horizontalInput > 0;
                bool isPulling = (facingRight && !movingRight) || (!facingRight && movingRight);

                animator.SetBool("isPulling", isPulling);
            }
            else
            {
                animator.speed = 0f;
                animator.SetBool("isPulling", false); // Pause push, no movement
            }
        }
        else
        {
            animator.speed = 1f;
            animator.SetBool("isPulling", false); // Not interacting
        }
    }
}