using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public class ForegroundHandler : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    private SpriteRenderer[] renderers;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SetAlpha(0f); // Fade out
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SetAlpha(1f); // Fade in
    }

    void SetAlpha(float targetAlpha)
    {
        foreach (var sr in renderers)
        {
            sr.DOFade(targetAlpha, fadeDuration);
        }
    }
}
