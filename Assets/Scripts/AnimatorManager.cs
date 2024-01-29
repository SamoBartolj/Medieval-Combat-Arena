using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool useRootMotion = false, bool isInvincible = false)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.SetBool("isUsingRootMotion", useRootMotion);
        animator.SetBool("isInvincible", isInvincible);
        animator.CrossFade(targetAnimation, 0.2f);
    }
}
