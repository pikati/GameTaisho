using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightFlowingWater : MonoBehaviour
{
    [SerializeField]
    private float flowSpeed;

    private DayNightChanger dnChanger;
    private FlowingWaterController fw;
    private GameObject flow;

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
        //InitFlowEffect();
    }

    private void InitFlowEffect()
    {
        flow = transform.Find("FlowingFlowEffect").gameObject;
        if(flow == null)
        {
            return;
        }
        ParticleSystem.MainModule main = flow.GetComponent<ParticleSystem>().main;
        ParticleSystem.MinMaxCurve mmc = main.startRotation;
        mmc.constant = Quaternion.Euler(0.0f, 0.0f, 270.0f - transform.rotation.eulerAngles.z).z;
        main.startRotation = mmc;
        Debug.Log(mmc.constant);
    }
}
