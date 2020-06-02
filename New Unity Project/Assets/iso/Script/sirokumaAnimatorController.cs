using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class sirokumaAnimatorController : MonoBehaviour
{
    private Animator anim;
    private float waittime;
    private bool waitFlag;
    private WaterHeightController waterline;
    private PlayerManager playerline;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        waittime = 0;
        waitFlag = false;
        waterline = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        playerline = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    void Update()
    {
        //Sleep
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("待機"))
        {
            if (!waitFlag)
            {
                anim.SetBool("goal1", false);
                anim.SetBool("goal2", false);
                anim.SetBool("bark", false);
                waittime = Time.time;
                waitFlag = true;
            }

            if (10.0f - (Time.time - waittime) < 0)
            {
                anim.SetBool("sleep", true);
            }
            
        }
        else
        {
            waitFlag = false;
        }



        //bark
        if (Keyboard.current.spaceKey.isPressed)
        {
            anim.SetBool("bark", true);
        }


        //swim
        if ((Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed) && playerline.position.y < waterline.waterHeight)
        {
            anim.SetBool("sleep", false);
            anim.SetBool("swim", true);
        }
        else
        {
            anim.SetBool("swim",false);
        } 



        //Goal
        if (Keyboard.current.gKey.isPressed) 
        {
            anim.SetBool("goal1", true);
        }

        if (Keyboard.current.lKey.isPressed)
        {
            anim.SetBool("goal2", true);
        }



        //walk
        if (( Keyboard.current.aKey.isPressed|| Keyboard.current.dKey.isPressed)&& playerline.position.y > waterline.waterHeight)
        {
            anim.SetBool("sleep", false);
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }



        //surprise
        if (Keyboard.current.digit1Key.isPressed)
        {
            anim.SetBool("surprise", true);
        }
        else
        {
            anim.SetBool("surprise", false);
        }



        //drown
        if (Keyboard.current.escapeKey.isPressed)
        {
            anim.SetBool("drown", true);
        }
        else
        {
            anim.SetBool("drown", false);
        }
    }
}
