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
            DontDestroyOnLoad(gameObject); // optional: keep between scenes
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

        foreach (var r in respawnables)
            r.SaveState();
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = lastCheckpointPosition;

        foreach (var r in respawnables)
            r.RestoreState();
    }
}
