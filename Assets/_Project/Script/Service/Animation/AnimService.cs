using UnityEngine;

public static class AnimService
{
    public static Animator animator;

    public static void SetAnimRun(bool isAnim)
    {
        if (animator == null)
        {
            Debug.Log("animator null");
        }

        // Debug.Log($"call set run = {isAnim} and animator = {animator == null}");

        animator.SetBool("isRunning", isAnim);
    }

    public static void SetAnimDie()
    {
        if (animator == null)
        {
            Debug.Log("animator null");
        }
        animator.SetTrigger("isDie");
    }
}