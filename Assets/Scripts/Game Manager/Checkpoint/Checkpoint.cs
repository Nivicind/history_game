using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool hasBeenActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenActivated)
        {
            hasBeenActivated = true;

            // Set this checkpoint as the new respawn position
            CheckpointManager.Instance.SetCheckpoint(transform.position);

            // Optional: Add visual or sound feedback
            Debug.Log("Checkpoint activated!");
        }
    }
}
