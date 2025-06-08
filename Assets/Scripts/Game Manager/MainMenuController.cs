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
    [SerializeField] private GameObject settingsPanel;

    private bool isSettingsOpen = false; // Tracks whether the settings panel is open

    private void Start()
    {
        // Ensure the settings panel is closed at the start
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Settings Panel is not assigned in MainMenuController!");
        }
    }
    void Update()
    {
        // Check for Esc key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettingsOpen)
            {
                CloseSettings();
            }
            else
            {
                OpenSettings();
            }
        }
    }

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

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            isSettingsOpen = true; // Update the state
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            isSettingsOpen = false; // Update the state
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked!");

        Application.Quit();

#if UNITY_EDITOR
#endif
    }
}