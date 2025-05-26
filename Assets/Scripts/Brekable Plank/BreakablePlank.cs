using System.Collections;
using UnityEngine;

public class BreakablePlank : MonoBehaviour
{
    private Collider2D objCollider;
    private Animator animator;
    private bool isBroken = false;

    void Start()
    {
        objCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    public void Break()
    {
        if (isBroken) return;

        isBroken = true;
        objCollider.enabled = false;
        animator.SetTrigger("Break");
        StartCoroutine(DisableAfterAnimation());
    }

    private IEnumerator DisableAfterAnimation()
    {
        float breakAnimLength = 0f;

        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Plank Breaking")
            {
                breakAnimLength = clip.length;
                break;
            }
        }

        yield return new WaitForSeconds(breakAnimLength);
        gameObject.SetActive(false);
    }

    public bool IsBroken()
    {
        return isBroken;
    }

    public void ResetPlank()
    {
        isBroken = false;
        objCollider.enabled = true;
        gameObject.SetActive(true);

        animator.ResetTrigger("Break");
        animator.Play("Plank Breaking", 0, 0f); // Replace "Idle" with your plank's default state name
    }

    public void BreakInstant()
    {
        isBroken = true;
        objCollider.enabled = false;
        gameObject.SetActive(false); // Skip animation, already broken
    }
}
