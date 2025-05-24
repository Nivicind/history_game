using UnityEngine;
using Cinemachine;
using System.Collections;

public class SwitchCamera : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera zoneCamera;
    public float zoneDuration = 2f; // how long to stay in the zone

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(SwitchToZoneCamera());
        }
    }

    private IEnumerator SwitchToZoneCamera()
    {
        zoneCamera.Priority = 20;
        mainCamera.Priority = 10;

        yield return new WaitForSeconds(zoneDuration);

        zoneCamera.Priority = 10;
        mainCamera.Priority = 20;
    }
}