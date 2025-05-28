using UnityEngine;
using UnityEngine.UI;
public class PlayerHideMechanic : MonoBehaviour
{
    private bool isHiding = false;
    private PlayerMovement playerMovement;

    [Header("Detection UI")]
    public Canvas DetectedCanvas;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        DetectedCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hide Object"))
        {
            if (playerMovement.isCrouching && !isHiding)
            {
                isHiding = true;
                Debug.Log("The player is hiding (from Stay)");
            }
            else if (!playerMovement.isCrouching && isHiding)
            {
                isHiding = false;
                Debug.Log("The player stood up and is no longer hiding");
            }
        }

        // Detector logic while overlapping
        if (collision.CompareTag("Detector") && !isHiding)
        {
            Debug.Log("The player has been detected!");
            PlayerGetDetected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hide Object") && isHiding)
        {
            isHiding = false;
            Debug.Log("The player exited hiding spot and is no longer hiding");
        }
    }

    private void PlayerGetDetected()
    {
        DetectedCanvas.gameObject.SetActive(true);
        playerMovement.enabled = false;
        Time.timeScale = 0f;
    }

    public void DisableDetectedCanvas()
    {
        DetectedCanvas.gameObject.SetActive(false);
        playerMovement.enabled = true;
    }
}
