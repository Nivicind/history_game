using UnityEngine;

public class RespawnableObject : MonoBehaviour, IRespawnable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        CheckpointManager.Instance.Register(this);
    }

    public void SaveState()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void LoadState()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    private void OnDestroy()
    {
        if (CheckpointManager.Instance != null)
            CheckpointManager.Instance.Unregister(this);
    }
}
