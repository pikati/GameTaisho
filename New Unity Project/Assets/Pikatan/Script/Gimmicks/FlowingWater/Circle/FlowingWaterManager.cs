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
            if (fwc[count] == null) fwc[count] = obj.GetComponent<StraightFlowController>();
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
            float height = flowingWaters[i].transform.position.y;
            GameObject s = flowingWaters[i].transform.GetChild(0).gameObject;
            //Straightに対応するためにStartPointが含まれていたら特別な処理
            if (s.name == "StartPoint")
            {
                GameObject e = flowingWaters[i].transform.GetChild(1).gameObject;
                float sh = s.transform.position.y;
                float eh = e.transform.position.y;
                height = sh < eh ? eh : sh;
                if(h >= height)
                {
                    if (day == fwc[i].IsVisibleDay())
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
            else
            {
                if (h - HEIGHT_DIFF >= height)
                {
                    if (day == fwc[i].IsVisibleDay())
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
