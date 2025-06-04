using UnityEngine;

public class Respawnable : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // Automatically register with the CheckpointManager on load
        if (CheckpointManager.Instance != null)
            CheckpointManager.Instance.RegisterRespawnable(this);

        SaveState(); // Capture initial state
    }

    public void SaveState()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void RestoreState()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
