using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [SerializeField]
    private float density;

    [SerializeField]
    private int referencePointNum;

    private WaterHeightController whc;
    private GameObject[] referencePoint;//足場の3つの基準点（何割浸水してるかを簡単に判断）（後で増やすかも）
    private int inWaterCount;//基準点が水面の中に入っている数

    public float buoyancy { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        referencePoint = new GameObject[referencePointNum];
        for(int i = 0; i < referencePointNum; i++)
        {
            referencePoint[i] = transform.GetChild(i).gameObject;
        }
        whc = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFlow();
    }

    private void UpdateFlow()
    {
        CountInWater();
        SetBuoyancy();
    }

    private void CountInWater()
    {
        inWaterCount = 0;
        float h = whc.waterHeight;
        for(int i = 0; i < referencePointNum; i++)
        {
            if(referencePoint[i].transform.position.y < h)
            {
                inWaterCount++;
            }
        }
    }

    private void SetBuoyancy()
    {
        float proportion = 0;
        if(inWaterCount != 0)
            proportion = inWaterCount / referencePointNum;
        buoyancy = 2 * 1 * proportion/* * -Physics.gravity.y * density*/;
    }

    public float GetPro()
    {
        float proportion = 1.0f;
        if (inWaterCount != 0)
            proportion = (referencePointNum - inWaterCount) / inWaterCount;
        return 2 * 1 * proportion;
    }
}
