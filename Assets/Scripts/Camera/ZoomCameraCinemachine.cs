using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ZoomCameraCinemachine : MonoBehaviour
{
    [Header("Zoom Settings")]
    public CinemachineVirtualCamera mainCamera;
    public float zoomInSize = 9f;
    public float zoomOutSize = 7f;
    public float zoomSpeed = 1f;

    [Header("Offset Settings")]
    public float enterXOffset = 5f;
    public float exitXOffset = 0f;
    public float offsetTweenSpeed = 1f;

    private CinemachineFramingTransposer transposer;
    private Tween zoomTween;
    private Tween offsetTween;

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not assigned!");
            return;
        }

        // Get the FramingTransposer component from the virtual camera
        transposer = mainCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (transposer == null)
        {
            Debug.LogError("Main camera is missing a CinemachineFramingTransposer component!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ZoomTo(zoomInSize);
            SetCameraXOffset(enterXOffset);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ZoomTo(zoomOutSize);
            SetCameraXOffset(exitXOffset);
        }
    }

    void ZoomTo(float targetSize)
    {
        if (zoomTween != null && zoomTween.IsActive())
            zoomTween.Kill();

        zoomTween = DOTween.To(
            () => mainCamera.m_Lens.OrthographicSize,
            x => mainCamera.m_Lens.OrthographicSize = x,
            targetSize,
            zoomSpeed
        ).SetEase(Ease.InOutSine);
    }

    void SetCameraXOffset(float targetOffset)
    {
        if (offsetTween != null && offsetTween.IsActive())
            offsetTween.Kill();

        offsetTween = DOTween.To(
            () => transposer.m_TrackedObjectOffset.x,
            x => {
                Vector3 newOffset = transposer.m_TrackedObjectOffset;
                newOffset.x = x;
                transposer.m_TrackedObjectOffset = newOffset;
            },
            targetOffset,
            offsetTweenSpeed
        ).SetEase(Ease.InOutSine);
    }
}
