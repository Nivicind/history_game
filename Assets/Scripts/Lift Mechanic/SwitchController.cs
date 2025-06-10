using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Header("Lift Settings")]
    [SerializeField] private GameObject lift;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float detectionDistance = 0.05f;

    [Header("Animator")]
    [SerializeField] private Animator switchAnimator;

    [Header("Audio")]
    [SerializeField] private AudioSource liftAudioSource;

    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;

    private SpriteRenderer switchRenderer;
    private BoxCollider2D liftCollider;
    private bool playerInRange = false;

    void Start()
    {
        switchRenderer = GetComponent<SpriteRenderer>();
        liftCollider = lift.GetComponentInChildren<BoxCollider2D>();

        if (switchRenderer == null)
            Debug.LogWarning("No SpriteRenderer found on this object!");
        if (liftCollider == null)
            Debug.LogWarning("Lift child object has no BoxCollider2D!");
        if (switchAnimator == null)
            Debug.LogWarning("Animator not assigned!");
    }

    void Update()
    {
        if (!playerInRange || liftCollider == null)
        {
            SwitchAnimationHandler(false, false);
            HandleLiftSound(false);
            return;
        }

        HandleLiftMovement();
    }

    void HandleLiftMovement()
    {
        Vector2 currentPos = lift.transform.position;
        float targetY = currentPos.y;

        bool isMovingUp = Input.GetKey(KeyCode.Q) && currentPos.y < maxY;
        bool isMovingDown = Input.GetKey(KeyCode.E) && currentPos.y > minY;
        bool isTryingToMove = false;

        if (isMovingUp)
        {
            targetY += moveSpeed * Time.deltaTime;
            isTryingToMove = true;
        }
        else if (isMovingDown)
        {
            Vector2 boxSize = liftCollider.bounds.size;
            Vector2 boxCenter = (Vector2)lift.transform.position + Vector2.down * (boxSize.y / 2f + detectionDistance / 2f);

            RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, detectionDistance, obstacleLayers);
            Collider2D[] tagHits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);

            bool hasPlankBelow = false;
            foreach (Collider2D col in tagHits)
            {
                if (col.CompareTag("Plank"))
                {
                    hasPlankBelow = true;
                    Debug.Log("Plank detected below: " + col.name);
                    break;
                }
            }

            if (hit.collider == null && !hasPlankBelow)
            {
                targetY -= moveSpeed * Time.deltaTime;
                isTryingToMove = true;
            }
            else
            {
                Debug.Log("Obstacle or Plank below: " + (hit.collider != null ? hit.collider.name : "None"));
            }
        }

        targetY = Mathf.Clamp(targetY, minY, maxY);
        lift.transform.position = new Vector2(currentPos.x, targetY);

        SwitchAnimationHandler(isMovingDown, isMovingUp);
        HandleLiftSound(isTryingToMove);
    }

    void SwitchAnimationHandler(bool isMovingDown, bool isMovingUp)
    {
        if (switchAnimator == null) return;

        if (isMovingDown)
        {
            switchAnimator.SetBool("IsSpinning", true);
            switchAnimator.SetFloat("Speed", 1f);
        }
        else if (isMovingUp)
        {
            switchAnimator.SetBool("IsSpinning", true);
            switchAnimator.SetFloat("Speed", -1f);
        }
        else
        {
            switchAnimator.SetBool("IsSpinning", false);
        }
    }

    void HandleLiftSound(bool isTryingToMove)
    {
        if (isTryingToMove)
        {
            if (!liftAudioSource.isPlaying)
                liftAudioSource.Play();
        }
        else
        {
            if (liftAudioSource.isPlaying)
                liftAudioSource.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            UpdateSwitchSprite(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            SwitchAnimationHandler(false, false);
            HandleLiftSound(false);
            UpdateSwitchSprite(false);
        }
    }

    void UpdateSwitchSprite(bool isHovering)
    {
        if (switchRenderer != null)
        {
            switchRenderer.sprite = isHovering ? hoverSprite : normalSprite;
        }
    }
}
