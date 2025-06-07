using UnityEngine;

public class RespawnablePlank : MonoBehaviour, IRespawnable
{
    [SerializeField] private BreakablePlank breakablePlank;

    private bool wasBroken;

    private void Start()
    {
        CheckpointManager.Instance.Register(this);
    }

    private void OnDestroy()
    {
        if (CheckpointManager.Instance != null)
            CheckpointManager.Instance.Unregister(this);
    }

    public void SaveState()
    {
        wasBroken = breakablePlank.IsBroken();
    }

    public void LoadState()
    {
        if (wasBroken)
            breakablePlank.BreakInstant();
        else
            breakablePlank.ResetPlank();
    }
}
