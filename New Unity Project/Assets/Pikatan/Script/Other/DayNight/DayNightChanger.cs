using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DayNightChanger : MonoBehaviour
{
    public bool isDay { get; private set; } = true;
    private PlayerInputManager pManager;
    private DisplayDayNight ddn;
    private DayNightLighting dnLight;
    private FlowingWaterManager fwm;

    private void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
        ddn = GetComponent<DisplayDayNight>();
        dnLight = GetComponent<DayNightLighting>();
        fwm = GameObject.Find("FlowingWaterManager").GetComponent<FlowingWaterManager>();
    }
    void Update()
    {
        if (pManager.isChange)
        {
            isDay = !isDay;
            ddn.ChangeSky(isDay);
            dnLight.ChangeLight(isDay);
            fwm.SetVisibleFlowingWaters();
        }
    }
}
