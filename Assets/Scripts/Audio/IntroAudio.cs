using UnityEngine;

public class IntroAudio : MonoBehaviour
{
    public AudioClip introMusic;
    public AudioClip jungleAmbient;

    void Start()
    {
        AudioManager.Instance.PlayMusic(introMusic);
        AudioManager.Instance.PlayAmbient(jungleAmbient);
    }
}
