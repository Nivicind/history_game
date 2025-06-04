using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.Playables; // THÊM DÒNG NÀY để làm việc với Timeline

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;     
    public PlayableDirector transitionTimelineDirector; 
    public string introSceneName = "Intro"; 

    void Start()
    {
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        // Ẩn các nút menu chính ngay lập tức để tránh người dùng click lại

        if (transitionTimelineDirector != null)
        {
            transitionTimelineDirector.Play();
            transitionTimelineDirector.stopped += OnTransitionTimelineFinished;
            Debug.Log("Playing transition timeline...");
        }
        else
        {
            Debug.LogWarning("Transition Timeline Director not assigned! Transitioning directly to Intro Scene.");
            SceneManager.LoadScene(introSceneName); 
        }
    }

    private void OnTransitionTimelineFinished(PlayableDirector director)
    {
        director.stopped -= OnTransitionTimelineFinished; 
        SceneManager.LoadScene(introSceneName); 
        Debug.Log("Transition timeline finished. Loading Intro Scene.");
    }

    public void OpenSettings()
    {
        Debug.Log("Opening Settings (Not implemented yet, or opens a panel)");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game!");
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}