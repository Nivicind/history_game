using UnityEngine;

public class PlayerHideMechanic : MonoBehaviour
{
    private bool isHiding = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
}
