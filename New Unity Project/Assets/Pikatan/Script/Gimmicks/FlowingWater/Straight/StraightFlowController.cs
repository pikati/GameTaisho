using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightFlowController : FlowingWaterController
{
    private GameObject flow;
    private GameObject arrow;

    // Start is called before the first frame update
    private void Awake()
    {
        CheckDayNgiht();
        GetFlowEffect();
        GetArrowObject();
        SetVisibleArrow();
    }

    private void GetFlowEffect()
    {
        flow = transform.Find("FlowingBubble").gameObject;
    }

    private void GetArrowObject()
    {
        arrow = transform.Find("Arrow").gameObject;
    }

    private void SetVisibleArrow()
    {
        if(!debug)
        {
            arrow.SetActive(false);
        }
    }
}
