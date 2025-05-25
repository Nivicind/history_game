using UnityEngine;
using UnityEngine.Playables;

public class TimelineMananger : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public MonoBehaviour playerController; // Drag your movement script here in the inspector

    void Start()
    {
        if (timelineDirector != null && playerController != null)
        {
            // Disable control at the start
            playerController.enabled = false;

            // Re-enable control when timeline ends
            timelineDirector.stopped += OnTimelineFinished;
        }
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        playerController.enabled = true;
    }
}
