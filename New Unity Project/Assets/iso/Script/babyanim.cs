using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyanim : MonoBehaviour
{
    private Animator anim;
    private WaterHeightController waterline;
    private StageEndJudge sej;
    private float posy;
    private bool happy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("helpLand", true);
        happy = false;
        waterline = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        sej = GameObject.Find("StageEndJudge").GetComponent<StageEndJudge>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sej.isGameClear) happy = true;
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
