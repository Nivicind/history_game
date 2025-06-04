using UnityEngine;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector2 lastCheckpointPosition;
    private List<Respawnable> respawnables = new List<Respawnable>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep manager alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterRespawnable(Respawnable obj)
    {
        if (!respawnables.Contains(obj))
            respawnables.Add(obj);
    }

    public void SetCheckpoint(Vector2 pos)
    {
        lastCheckpointPosition = pos;

        // Clean up destroyed references
        respawnables.RemoveAll(r => r == null);

        foreach (var r in respawnables)
            r.SaveState();
    }

    public void RespawnPlayer(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("RespawnPlayer called but player is null.");
            return;
        }

        player.transform.position = lastCheckpointPosition;

        // Clean up destroyed references before restoring
        respawnables.RemoveAll(r => r == null);

        foreach (var r in respawnables)
            r.RestoreState();
    }
}
