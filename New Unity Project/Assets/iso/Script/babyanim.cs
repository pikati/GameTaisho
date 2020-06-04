using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyanim : MonoBehaviour
{
    private Animator anim;
    private WaterHeightController waterline;
    private float posy;
    private bool happy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        happy = false;
        waterline = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (happy)
        {
            anim.SetBool("happy", true);
            anim.SetBool("helpSea", false);
            anim.SetBool("helpLand", false);
        }
        else
        {
            posy = transform.position.y;
            if (posy > waterline.waterHeight)
            {
                anim.SetBool("helpSea", false);
                anim.SetBool("helpLand", true);
            }
            else
            {
                anim.SetBool("helpLand", false);
                anim.SetBool("helpSea", true);
            }

        }
    }
}
