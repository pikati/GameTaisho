using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingWaterController : MonoBehaviour
{
    private const int WATER_NUM = 4;
    private GameObject[] flowingWaters;
    private GameObject[] arrows;
    private GameObject[] flows;

    [SerializeField]
    private bool debug;

    [SerializeField]
    private bool reverse;

    [SerializeField]
    private bool isDay;

    [SerializeField]
    private bool isNight;
    void Awake()
    {
        CheckDayNgiht();
        GetFlowingWaterObject();
        GetArrowObject();
        GetFlowEffect();
        SetVisibleArrow();
        SetReverseArrow();
        SetReverseFlowEffect();
    }

    private void CheckDayNgiht()
    {
        if (isDay && isNight || !isDay && !isNight)
        {
            Debug.LogError("Please make one of isDay or isNight true.(isDayかisNightのどちらかをtrueにしてください)");
        }
    }

    private void GetFlowingWaterObject()
    {
        flowingWaters = new GameObject[WATER_NUM];
        flowingWaters[0] = transform.GetChild(0).gameObject;
        flowingWaters[1] = transform.GetChild(1).gameObject;
        flowingWaters[2] = transform.GetChild(2).gameObject;
        flowingWaters[3] = transform.GetChild(3).gameObject;
    }

    private void GetArrowObject()
    {
        arrows = new GameObject[WATER_NUM];
        for (int i = 0; i < WATER_NUM; i++)
        {
            arrows[i] = flowingWaters[i].transform.Find("Arrow").gameObject;
        }
    }

    private void GetFlowEffect()
    {
        flows = new GameObject[WATER_NUM];
        for (int i = 0; i < WATER_NUM; i++)
        {
            flows[i] = flowingWaters[i].transform.Find("FlowingBubble").gameObject;
        }
    }

    private void SetVisibleArrow()
    {
        if (debug) return;

        for (int i = 0; i < WATER_NUM; i++)
        {
            arrows[i].SetActive(false);
        }
    }

    private void SetReverseArrow()
    {
        if (!reverse) return;

        for(int i = 0; i < WATER_NUM; i++)
        {
            Vector3 scl = arrows[i].transform.localScale;
            scl.y *= -1;
            arrows[i].transform.localScale = scl;
        }
    }

    private void SetReverseFlowEffect()
    {
        if (!reverse) return;

        for (int i = 0; i < WATER_NUM; i++)
        {
            if (i < 2)
            {
                flows[i].transform.rotation = Quaternion.Euler(new Vector3(180.0f, 0f, 0f));
            }
            else
            {
                flows[i].transform.rotation = Quaternion.Euler(new Vector3(0f, 180.0f, 0f));
            }
        }
    }

    public bool IsReverse()
    {
        return reverse;
    }

    public bool IsVisibleDay()
    {
        return isDay;
    }
}
