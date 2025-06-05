using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private Vector3 checkpointPosition;
    private List<IRespawnable> respawnables = new List<IRespawnable>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Register(IRespawnable respawnable)
    {
        if (!respawnables.Contains(respawnable))
            respawnables.Add(respawnable);
    }

    public void Unregister(IRespawnable respawnable)
    {
        respawnables.Remove(respawnable);
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        foreach (var r in respawnables)
        {
            r.SaveState();
        }
    }

    public void Respawn()
    {
        foreach (var r in respawnables)
        {
            r.LoadState();
        }
    }

    public Vector3 GetCheckpointPosition()
    {
        return checkpointPosition;
    }
}
