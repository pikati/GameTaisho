using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameclearicebreak : MonoBehaviour
{
    protected float starttime;
    private GameObject[] objice;
    private int num;
    private Transform tr;
    private PlayerManager Pman;//player
    private StageEndJudge Clear;
    private bool flag;
    private int[] turn = { 0, 12, 4, 5, 15, 9, 11, 13, 2, 14, 1, 3, 6, 7, 10, 8 };
    private GameObject[] penguins;
    private Animator[] animators;

                               // Start is called before the first frame update
    void Start()
    {
        starttime = Time.time;
        objice = new GameObject[17];
        objice = GameObject.FindGameObjectsWithTag("breakwall");
        num = 0;
        tr = transform;
        Pman = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        Clear = GameObject.Find("StageEndJudge").GetComponent<StageEndJudge>();
        flag = false;
        penguins = new GameObject[6];
        penguins = GameObject.FindGameObjectsWithTag("BabyDrill");
        animators = new Animator[6];

        for (int i = 0; i < 5; i++)
        {
            animators[i] = penguins[i].GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Clear.isGameClear)
        {
            
            if (!flag)
            {
               
                //Debug.Log(animators[i]);
                
                tr.position = new Vector3(Pman.position.x - 4.0f, Pman.position.y + 6.2f, -18.0f);
                flag = true;
            }

            if (Time.time - starttime >= 0.1f && num < 16)
            {
                starttime = Time.time;
              
                Destroy(objice[turn[num]]);
                num++;
            }
            if (num == 16)
            {
                for (int i = 0; i < 5; i++)
                {
                    animators[i].SetBool("tobu", true);
                }
            }
        }
    }
}
