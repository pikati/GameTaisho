using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveX : TutorialMove
{
    private PlayerInputManager pim;
    private bool isMove = true;

    private void Start()
    {
        base.Start();
        pim = GameObject.Find("sirokuma").GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PopUpImage();
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (pim.isCreate) isMove = false;
        if (pim.isIceDecide) isMove = true;
        if(isMove)
        {
            sr.sprite = s1;
        }
        else
        {
            sr.sprite = s2;
        }
    }
}
