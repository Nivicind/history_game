using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Audio : MonoBehaviour
{
    public AudioClip level2Music;

    void Start()
    {
        AudioManager.Instance.PlayMusic(level2Music);
    }
}
