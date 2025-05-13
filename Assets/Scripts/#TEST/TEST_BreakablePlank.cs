using System.Collections;
using UnityEngine;

public class TEST_BreakablePlank : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string boxTag = "Box"; // Tag to detect the box
    [SerializeField] private float fallThreshold = -2f; // Velocity to trigger breaking

    private Collider2D objCollider;
    private Animator animator;
    private bool isBreaking = false;

    void Start()
    {
        objCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreaking) return;

        if (collision.gameObject.CompareTag(boxTag))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null && rb.velocity.y < fallThreshold)
            {
                isBreaking = true;
                objCollider.enabled = false; // Disable plank collision
                animator.SetTrigger("Break"); // Trigger the animation
                StartCoroutine(DisableAfterAnimation());
            }
        }
    }

    private IEnumerator DisableAfterAnimation()
    {
        // Wait for the animation clip to finish playing
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        float breakAnimLength = 0f;

        // Find the length of the Plank Breaking animation
        foreach (var clip in clips)
        {
            if (clip.name == "Plank Breaking") // Make sure this matches your clip name
            {
                breakAnimLength = clip.length;
                break;
            }
        }

        yield return new WaitForSeconds(breakAnimLength);
        gameObject.SetActive(false); // Disable the plank object after the animation
    }
}