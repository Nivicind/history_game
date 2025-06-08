using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Audio : MonoBehaviour
{
    public AudioClip level1Music;
    public AudioClip jungleAmbient;

    void Start()
    {
        AudioManager.Instance.PlayMusic(level1Music);
        AudioManager.Instance.PlayAmbient(jungleAmbient);
    }
}
