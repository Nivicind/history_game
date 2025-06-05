using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public GameObject savingText;
    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasActivated) return;

        if (collision.CompareTag("Player"))
        {
            hasActivated = true;
            CheckpointManager.Instance.SetCheckpoint(transform.position);
            if (savingText != null)
                StartCoroutine(ShowSavingText());
        }
    }

    private IEnumerator ShowSavingText()
    {
        savingText.SetActive(true);
        yield return new WaitForSeconds(1f);
        savingText.SetActive(false);
    }
}
