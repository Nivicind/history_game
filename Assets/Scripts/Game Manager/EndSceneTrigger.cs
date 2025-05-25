using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndSceneTrigger : MonoBehaviour
{
    public PlayableDirector outroTimeline;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            outroTimeline.Play();
        }
    }
}
