using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Timeline and Scene")]
    public PlayableDirector exitCutscene;
    public string nextSceneName;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (exitCutscene != null)
            {
                exitCutscene.Play();
                exitCutscene.stopped += OnCutsceneFinished;
            }
            else
            {
                Debug.LogWarning("No exit cutscene assigned. Loading next scene immediately.");
                LoadNextScene();
            }
        }
    }

    private void OnCutsceneFinished(PlayableDirector director)
    {
        director.stopped -= OnCutsceneFinished;
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not assigned!");
        }
    }
}
