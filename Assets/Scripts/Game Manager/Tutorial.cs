using UnityEngine;
using DG.Tweening;

public class TutorialZoneDOTween : MonoBehaviour
{
    [Header("Tutorial Sprite")]
    public GameObject tutorialSprite;
    public Vector3 followOffset = new Vector3(0, 2, 0);
    public float fadeDuration = 0.5f;

    private Transform player;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (tutorialSprite != null)
        {
            spriteRenderer = tutorialSprite.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                spriteRenderer.color = new Color(1, 1, 1, 0); // Start fully transparent
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    private void Update()
    {
        if (tutorialSprite != null && player != null)
        {
            tutorialSprite.transform.position = player.position + followOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spriteRenderer != null)
        {
            spriteRenderer.DOFade(1f, fadeDuration);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spriteRenderer != null)
        {
            spriteRenderer.DOFade(0f, fadeDuration);
        }
    }
}
