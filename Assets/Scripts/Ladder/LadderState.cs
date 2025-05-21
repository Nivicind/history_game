using UnityEngine;

public class LadderState : MonoBehaviour
{
    public enum LadderVisualState { Idle, Hover, Active }
    private LadderVisualState currentState;

    [System.Serializable]
    public class LadderVisualSprites
    {
        public Sprite top1;
        public Sprite top2;
        public Sprite top3;
        public Sprite body;
        public Sprite bottom1;
        public Sprite bottom2;
    }

    [Header("Ladder Direction")]
    public bool facingRightLadder = true;
    [Header("State Sprites")]
    public LadderVisualSprites idleSprites;
    public LadderVisualSprites hoverSprites;
    public LadderVisualSprites activeSprites;

    [Header("Ladder Piece References (in order)")]
    public SpriteRenderer top1Renderer;
    public SpriteRenderer top2Renderer;
    public SpriteRenderer top3Renderer;
    public SpriteRenderer bodyRenderer; // This should be a TILED sprite
    public SpriteRenderer bottom1Renderer;
    public SpriteRenderer bottom2Renderer;

    public void SetLadderState(LadderVisualState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        UpdateLadderSprites();
    }

    private void UpdateLadderSprites()
    {
        LadderVisualSprites spritesToUse = idleSprites;

        switch (currentState)
        {
            case LadderVisualState.Hover:
                spritesToUse = hoverSprites;
                break;
            case LadderVisualState.Active:
                spritesToUse = activeSprites;
                break;
        }

        top1Renderer.sprite = spritesToUse.top1;
        top2Renderer.sprite = spritesToUse.top2;
        top3Renderer.sprite = spritesToUse.top3;
        bodyRenderer.sprite = spritesToUse.body;
        bottom1Renderer.sprite = spritesToUse.bottom1;
        bottom2Renderer.sprite = spritesToUse.bottom2;
    }
}
