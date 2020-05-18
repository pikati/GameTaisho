using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlowingWaterManager : MonoBehaviour
{
    private GameObject[] flowingWaters;
    private FlowingWaterController[] fwc;
    private WaterHeightController whc;
    private DayNightChanger dnChanger;

    private const float HEIGHT_DIFF = 4.0f;

    void Start()
    {
        dnChanger = GameObject.Find("DayNightChanger").GetComponent<DayNightChanger>();
        flowingWaters = GameObject.FindGameObjectsWithTag("FlowingWater");
        whc = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        fwc = new FlowingWaterController[flowingWaters.Length];

        int count = 0;

        foreach (GameObject obj in flowingWaters)
        {
            fwc[count] = obj.GetComponent<FlowingWaterController>();
            count++;
        }
    }

    private void Update()
    {
        float[] pos = new float[flowingWaters.Length];
        GetFWHeight(ref pos);
        SetActiveFW(pos);
    }

    private void GetFWHeight(ref float[] pos)
    {
        for(int i = 0; i < flowingWaters.Length; i++)
        {
            pos[i] = flowingWaters[i].transform.position.y;
        }
    }

    private void SetActiveFW(float[] pos)
    {
        float h = whc.waterHeight;
        bool day = dnChanger.isDay;
        for (int i = 0; i < flowingWaters.Length; i++)
        {
            if(h - HEIGHT_DIFF >= flowingWaters[i].transform.position.y)
            {
                if(day == fwc[i].IsVisibleDay())
                {
                    flowingWaters[i].SetActive(true);

                }
                else
                {
                    SetFalse(i);
                }
            }
            else
            {
                SetFalse(i);
            }
        }
    }

    private void SetFalse(int i)
    {
        bool a = flowingWaters[i].activeSelf;
        flowingWaters[i].SetActive(false);
        if (a != flowingWaters[i].activeSelf)
        {
            GameObject[] flows = GameObject.FindGameObjectsWithTag("Move");
            foreach (GameObject obj in flows)
            {
                obj.GetComponent<FlowObjectController>().ResetDir();
            }
        }
    }
}
