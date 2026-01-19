using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void SetFacing(float dir)
    {
        if (dir < 0f)
            transform.eulerAngles = Vector3.zero;
        else if (dir > 0f)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    public void SetIdle(bool idle)
    {
        anim.SetBool("IsIdle", idle);
    }

    public void SetDead()
    {
        anim.SetTrigger("Dead");
    }
}
