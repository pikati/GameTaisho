﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public AudioSource footStep;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartWalk()
    {
        animator.SetBool("walk", true);
        footStep.enabled = true;
        footStep.loop = true;
    }

    public void EndWalk()
    {
        footStep.enabled = false;
        footStep.loop = false;
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

    public void StartBark()
    {
        animator.SetBool("bark", true);
    }

    public void EndBark()
    {
        animator.SetBool("bark", false);
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
