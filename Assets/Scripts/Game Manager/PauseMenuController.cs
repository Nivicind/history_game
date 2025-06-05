using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
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
