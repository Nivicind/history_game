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

    [Header("Lookahead Settings")]
    public float enterLookaheadTime = 1f;
    public float exitLookaheadTime = 0f;
    public float lookaheadTweenSpeed = 1f;

    private CinemachineFramingTransposer transposer;
    private Tween zoomTween;
    private Tween lookaheadTween;

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
            SetLookaheadTime(enterLookaheadTime);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ZoomTo(zoomOutSize);
            SetLookaheadTime(exitLookaheadTime);
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

    void SetLookaheadTime(float targetTime)
    {
        if (lookaheadTween != null && lookaheadTween.IsActive())
            lookaheadTween.Kill();

        lookaheadTween = DOTween.To(
            () => transposer.m_LookaheadTime,
            x => transposer.m_LookaheadTime = x,
            targetTime,
            lookaheadTweenSpeed
        ).SetEase(Ease.InOutSine);
    }
}