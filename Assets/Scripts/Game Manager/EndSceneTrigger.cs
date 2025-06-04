using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; 
using UnityEngine.SceneManagement; 

public class EndSceneTrigger : MonoBehaviour
{
    public PlayableDirector outroTimeline; 

    [Header("Next Scene Settings")]
    public string nextSceneName; 

    private bool hasTriggered = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            if (outroTimeline != null)
            {
                outroTimeline.Play();                 
                outroTimeline.stopped += OnOutroTimelineFinished;
                Debug.Log("Outro timeline started.");
            }
            else
            {
                Debug.LogWarning("Outro Timeline is not assigned to EndSceneTrigger! Transitioning directly to next scene.");
                LoadNextScene();
            }
        }
    }

    private void OnOutroTimelineFinished(PlayableDirector director)
    {
        director.stopped -= OnOutroTimelineFinished; 
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("Loading next scene: " + nextSceneName);
        }
        else
        {
            Debug.LogError("Next Scene Name is not set in EndSceneTrigger!");
        }
    }
}