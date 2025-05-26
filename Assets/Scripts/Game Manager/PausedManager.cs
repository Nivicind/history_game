using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            pauseMenuCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            pauseMenuCanvas.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
    }
}
