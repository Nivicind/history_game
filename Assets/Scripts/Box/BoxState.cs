using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxVisualState
{
    Idle,
    Hover,
    Dragged
}

public class BoxState : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite hoverSprite;
    public Sprite draggedSprite;

    private SpriteRenderer spriteRenderer;
    private BoxVisualState currentState;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetState(BoxVisualState.Idle); // Default state
    }

    public void SetState(BoxVisualState newState)
    {
        if (currentState == newState) return; // Avoid re-setting the same sprite

        currentState = newState;

        switch (currentState)
        {
            case BoxVisualState.Idle:
                spriteRenderer.sprite = idleSprite;
                break;
            case BoxVisualState.Hover:
                spriteRenderer.sprite = hoverSprite;
                break;
            case BoxVisualState.Dragged:
                spriteRenderer.sprite = draggedSprite;
                break;
        }
    }
}
