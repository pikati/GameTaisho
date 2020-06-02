using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartWalk()
    {
        animator.SetBool("walk", true);
    }

    public void EndWalk()
    {
        animator.SetBool("walk", false);
    }

    public void StartSwim()
    {
        animator.SetBool("swim", true);
    }

    public void EndSwim()
    {
        animator.SetBool("swim", false);
    }

    public void Bark()
    {
        animator.SetBool("bark", true);
    }

    public void StartSurprised()
    {
        animator.SetBool("surprise", true);
    }

    public void EndSurprised()
    {
        animator.SetBool("surprise", false);
    }

    public void Goal()
    {
        animator.SetBool("goal1", true);
    }

    public void LongGoal()
    {
        animator.SetBool("goal2", true);
    }

    public void Drown()
    {
        animator.SetBool("drown", true);
    }

    public void Sleep()
    {
        animator.SetBool("sleep", true);
    }
}
