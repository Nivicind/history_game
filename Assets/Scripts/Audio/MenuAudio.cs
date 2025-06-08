using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public AudioClip menuMusic;

    void Start()
    {
        AudioManager.Instance.PlayMusic(menuMusic);
    }
}
