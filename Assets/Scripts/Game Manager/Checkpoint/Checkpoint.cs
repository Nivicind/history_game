using UnityEngine;
using TMPro; // Don't forget this namespace for TextMeshPro
using System.Collections; // Required for Coroutines

public class Checkpoint : MonoBehaviour
{
    private bool hasBeenActivated = false;

    [Header("UI References")]
    public TextMeshProUGUI savingTextUI; // Drag your "SavingText" UI element here in the Inspector
    public float displayDuration = 2.0f; // How long the "Saving..." text stays visible

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenActivated)
        {
            hasBeenActivated = true;

            // Set this checkpoint as the new respawn position
            CheckpointManager.Instance.SetCheckpoint(transform.position);

            // Notify player about saving
            ShowSavingText();

            // Optional: Add visual or sound feedback
            Debug.Log("Checkpoint activated!");
        }
    }

    private void ShowSavingText()
    {
        if (savingTextUI != null)
        {
            savingTextUI.gameObject.SetActive(true); // Make the text visible
            // You can also change the text here if needed, e.g., savingTextUI.text = "Saving...";
            StartCoroutine(HideSavingTextAfterDelay(displayDuration)); // Start coroutine to hide it
        }
        else
        {
            Debug.LogWarning("Saving Text UI reference not set in Checkpoint script!");
        }
    }

    private IEnumerator HideSavingTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified duration
        if (savingTextUI != null)
        {
            savingTextUI.gameObject.SetActive(false); // Hide the text
        }
    }
}