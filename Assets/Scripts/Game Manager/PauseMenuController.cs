using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsUI;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);
    
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                if (settingsUI.activeSelf)
                {
                    CloseSettings();
                }
                else
                {
                    Resume();
                }
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        settingsUI.SetActive(false); // Ensure only pause menu shows first
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenSettings()
    {
        settingsUI.SetActive(true);
        // pauseMenuUI stays active underneath
    }

    public void CloseSettings()
    {
        settingsUI.SetActive(false);
        // pauseMenuUI is still active
    }

    public void RestartFromCheckpoint()
    {
        Time.timeScale = 1f;
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.Respawn();
        }
        else
        {
            Debug.LogWarning("CheckpointManager not found.");
        }

        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        isPaused = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
