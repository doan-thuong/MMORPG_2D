using UnityEngine;

public class EnemyAnimatorService : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimRun(bool isRun)
    {
        if (anim == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
            return;
        }
        anim.SetBool("isRun", isRun);
    }

    public void SetAnimAttack()
    {
        if (anim == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
            return;
        }
        anim.SetTrigger("isAttack");
    }

    public void SetAnimGetHit()
    {
        if (anim == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
            return;
        }
        anim.SetTrigger("getHit");
    }

    public void SetAnimGetFinalDamage()
    {
        if (anim == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
            return;
        }
        anim.SetTrigger("finalDamage");
    }

    public void SetAnimDeath()
    {
        if (anim == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
            return;
        }
        anim.SetTrigger("isDeath");
    }
}