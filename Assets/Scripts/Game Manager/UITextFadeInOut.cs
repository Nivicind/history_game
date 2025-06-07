using UnityEngine;
using DG.Tweening;
using TMPro;

public class UITextFadeInOut : MonoBehaviour
{
    [Header("UI Tutorial Text")]
    public TextMeshProUGUI tutorialText; // Assign your TMP text here
    public float fadeDuration = 0.5f;

    private void Start()
    {
        if (tutorialText != null)
        {
            var color = tutorialText.color;
            color.a = 0f;
            tutorialText.color = color; // Start hidden
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && tutorialText != null)
        {
            tutorialText.DOFade(1f, fadeDuration);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && tutorialText != null)
        {
            tutorialText.DOFade(0f, fadeDuration);
        }
    }
}