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

    private GameObject currentBox;
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
        boxRb.bodyType = RigidbodyType2D.Dynamic;

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

        if (currentBox != null)
        {
            Rigidbody2D boxRb = currentBox.GetComponent<Rigidbody2D>();
            boxRb.bodyType = RigidbodyType2D.Static;
        }

        currentBox = null;
        IsAttachedToBox = false;
        isDraggingBox = false;
    }

    void AutoDetach()
    {
        bool playerNotGrounded = !playerMovement.isGrounded;
        bool boxNotGrounded = false;

        if (IsAttachedToBox && currentBox != null)
        {
            BoxState state = currentBox.GetComponent<BoxState>();
            if (state != null && state.boxGroundCheck != null)
            {
                Collider2D boxGroundCollider = state.boxGroundCheck.GetComponent<Collider2D>();
                if (boxGroundCollider != null)
                {
                    boxNotGrounded = !boxGroundCollider.IsTouchingLayers(state.groundLayer);
                }
            }
        }

        if (IsAttachedToBox && (playerNotGrounded || boxNotGrounded))
        {
            DetachBox();
        }
    }
    void UpdateBoxVisual()
    {
        GameObject[] allBoxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject box in allBoxes)
        {
            BoxState state = box.GetComponent<BoxState>();
            if (state == null) continue;

            if (IsAttachedToBox && currentBox == box)
            {
                state.SetState(BoxVisualState.Dragged);
            }
            else if (!IsAttachedToBox && currentBox == box)
            {
                state.SetState(BoxVisualState.Hover);
            }
            else
            {
                state.SetState(BoxVisualState.Idle);
            }
        }
    }

    void PushPullAnimationHandler()
    {
        if (animator != null)
        {
            animator.SetBool("isPushing", IsAttachedToBox);
        }
    }
}
