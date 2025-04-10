using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushBoxes : MonoBehaviour
{
    public Transform pushPullCheck;      // Reference to a point in front of the player
    public LayerMask boxLayer;           // Layer for boxes

    private GameObject currentBox;
    private FixedJoint2D joint;

    public Animator animator;
    public bool isAttached = false;
    public bool isDraggingBox { get; private set; } = false;

    void Update()
    {
        if (!isAttached)
        {
            CheckForBox();
        }

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
            if (!isAttached && currentBox != null)
            {
                AttachBox();
            }
            else if (isAttached)
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

        // Set anchor to player's center
        joint.anchor = Vector2.zero;

        // Calculate the connected anchor in box's local space
        Vector2 connectedAnchor = boxRb.transform.InverseTransformPoint(transform.position);
        joint.connectedAnchor = connectedAnchor;

        isAttached = true;
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
        isAttached = false;
        isDraggingBox = false;
    }

    void UpdateBoxVisual()
    {
        // Find all boxes
        GameObject[] allBoxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject box in allBoxes)
        {
            BoxState state = box.GetComponent<BoxState>();
            if (state == null) continue;

            // If box is currently being dragged
            if (isAttached && currentBox == box)
            {
                state.SetState(BoxVisualState.Dragged);
            }
            // If box is currently hovered (in pushPullCheck range)
            else if (!isAttached && currentBox == box)
            {
                state.SetState(BoxVisualState.Hover);
            }
            // Otherwise, it's idle
            else
            {
                state.SetState(BoxVisualState.Idle);
            }
        }
    }

    void PushPullAnimationHandler(){
        if (animator != null)
        {
            animator.SetBool("isPushing", isAttached);
        }
    }
}
