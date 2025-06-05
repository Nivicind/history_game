using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for Coroutines

// Required for PlayableDirector
using UnityEngine.Playables;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Transitions")]
    [SerializeField] private string introSceneName = "Intro"; // Name of the Intro scene

    [Header("Fade Out References")]
    public PlayableDirector fadeOutTimelineDirector; 
    [SerializeField] private float fadeDuration = 1.0f; 
    public void PlayGame()
    {
        Debug.Log("Play Game button clicked! Initiating fade.");

        StartCoroutine(FadeAndLoadScene());
    }

    private IEnumerator FadeAndLoadScene()
    {

        if (fadeOutTimelineDirector != null)
        {
            fadeOutTimelineDirector.time = 0;
            fadeOutTimelineDirector.Play(); // Play the timeline
        }
        else
        {
            Debug.LogError("Fade Out Timeline Director is not assigned in MainMenuController!");
            yield return null;
        }

        yield return new WaitForSeconds(fadeDuration);

        Debug.Log("Fade complete. Loading Intro scene.");
        SceneManager.LoadScene(introSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked!");

        Application.Quit();

        #if UNITY_EDITOR
        #endif
    }
}