using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlowingWaterManager : MonoBehaviour
{
    private GameObject[] flowingWaters;
    private FlowingWaterController[] fwc;

    private DayNightChanger dnChanger;

    void Start()
    {
        dnChanger = GameObject.Find("DayNightChanger").GetComponent<DayNightChanger>();
        flowingWaters = GameObject.FindGameObjectsWithTag("FlowingWater");

        fwc = new FlowingWaterController[flowingWaters.Length];

        int count = 0;

        foreach (GameObject obj in flowingWaters)
        {
            fwc[count] = obj.GetComponent<FlowingWaterController>();
            count++;
        }

        SetVisibleFlowingWaters();
    }

    public void SetVisibleFlowingWaters()
    {
        for (int i = 0; i < flowingWaters.Length; i++)
        {
            if(dnChanger.isDay)
            {
                if(fwc[i].IsVisibleDay())
                {
                    flowingWaters[i].SetActive(true);
                }
                else
                {
                    flowingWaters[i].SetActive(false);
                }
            }
            else
            {
                if (fwc[i].IsVisibleDay())
                {
                    flowingWaters[i].SetActive(false);
                }
                else
                {
                    flowingWaters[i].SetActive(true);
                }
            }
        }
    }
}
