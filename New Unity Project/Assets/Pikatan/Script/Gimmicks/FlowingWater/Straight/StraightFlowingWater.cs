using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightFlowingWater : MonoBehaviour
{
    [SerializeField]
    private float flowSpeed;

    private DayNightChanger dnChanger;
    private FlowingWaterController fw;

    public FlowDir dir { get; private set; }

    public float speed { get; private set; }

    public float angle { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        speed = flowSpeed;
        dnChanger = GameObject.Find("DayNightChanger").GetComponent<DayNightChanger>();
        fw = transform.parent.gameObject.GetComponent<FlowingWaterController>();

        dir = FlowDir.STRAIGHT;
        angle = transform.parent.localEulerAngles.z;
    }
}
