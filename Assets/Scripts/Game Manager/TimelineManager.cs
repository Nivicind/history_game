using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [Header("Start of Level Timeline")]
    public PlayableDirector introTimelineDirector; // Assign your intro timeline here

    [Header("End of Level Timeline")]
    public PlayableDirector outroTimelineDirector; // Assign your outro timeline here

    [Header("Player Control")]
    public MonoBehaviour playerController; // Drag your movement script here in the inspector

    void Start()
    {
        // --- Handle Intro Timeline ---
        if (introTimelineDirector != null && playerController != null)
        {
            // Disable control at the start if the intro timeline is set
            playerController.enabled = false;

            // Re-enable control when the intro timeline ends
            introTimelineDirector.stopped += OnIntroTimelineFinished;
        }
        else if (playerController != null)
        {
            // If no intro timeline, ensure player control is enabled by default
            playerController.enabled = true;
        }

        if (outroTimelineDirector != null)
        {
            outroTimelineDirector.stopped += OnOutroTimelineFinished;
        }
    }

    // Called when the intro timeline finishes
    void OnIntroTimelineFinished(PlayableDirector director)
    {
        if (playerController != null)
        {
            playerController.enabled = true; // Re-enable player control
            Debug.Log("Intro timeline finished. Player control re-enabled.");
        }
    }

    // Called when the outro timeline finishes
    void OnOutroTimelineFinished(PlayableDirector director)
    {
        if (playerController != null)
        {
            playerController.enabled = false; // Keep player control disabled
            Debug.Log("Outro timeline finished. Player control remains disabled for level end.");
            // Add any end-of-level logic here, e.g., loading next scene, showing credits.
        }
    }

    // --- Public method to trigger the outro timeline and disable player control ---
    // You'll call this from another script when the level end condition is met.
    public void TriggerOutroTimeline()
    {
        if (outroTimelineDirector != null && playerController != null)
        {
            playerController.enabled = false; // Disable control *before* playing outro
            outroTimelineDirector.Play();
            Debug.Log("Outro timeline triggered. Player control disabled.");
        }
        else if (outroTimelineDirector == null)
        {
            Debug.LogWarning("Outro timeline not assigned in TimelineManager. Cannot play.");
        }
        else if (playerController == null)
        {
            Debug.LogError("Player Controller not assigned in TimelineManager!");
        }
    }
}