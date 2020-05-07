using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FlowingWater : MonoBehaviour
{
    [SerializeField]
    private float flowSpeed;

    

    private DayNightChanger dnChanger;
    private FlowingWaterController fw;

    public FlowDir dir { get; private set; }

    public float speed { get; private set; }
    void Start()
    {
        speed = flowSpeed;
        dnChanger = GameObject.Find("DayNightChanger").GetComponent<DayNightChanger>();
        fw = transform.parent.gameObject.GetComponent<FlowingWaterController>();
        
        SetDir();
    }


    private void SetDir()
    {
        bool reverse = fw.IsReverse();
        if(tag == "FlowUp")
        {
            if(!reverse)
            {
                dir = FlowDir.UP;
            }
            else
            {
                dir = FlowDir.DOWN;
            }
        }
        else if (tag == "FlowDown")
        {
            if (!reverse)
            {
                dir = FlowDir.DOWN;
            }
            else
            {
                dir = FlowDir.UP;
            }
        }
        else if (tag == "FlowRight")
        {
            if (!reverse)
            {
                dir = FlowDir.RIGHT;
            }
            else
            {
                dir = FlowDir.LEFT;
            }
        }
        else if (tag == "FlowLeft")
        {
            if (!reverse)
            {
                dir = FlowDir.LEFT;
            }
            else
            {
                dir = FlowDir.RIGHT;
            }
        }
        else
        {
            dir = FlowDir.NON;
        }
    }
}
