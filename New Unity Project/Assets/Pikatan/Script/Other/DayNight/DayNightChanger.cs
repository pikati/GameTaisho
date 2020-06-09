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
    [SerializeField]
    private bool isEnable;
    [SerializeField]
    private bool isDayTime;

    private void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
        ddn = GetComponent<DisplayDayNight>();
        dnLight = GetComponent<DayNightLighting>();
        fwm = GameObject.Find("FlowingWaterManager").GetComponent<FlowingWaterManager>();
        DayNightFade.OnEndFade += ChangeDayNight;
        isDay = isDayTime;
        ddn.ChangeSky(isDay);
    }
    void Update()
    {
        if (!isEnable) return;
        if (pManager.isChange)
        {
            FindObjectOfType<AudioManager>().PlaySound("DayNight", 0);
            FindObjectOfType<AudioManager>().PlaySound("Thema", 0);
            DayNightFade.FadeIn();
        }
    }

    private void ChangeDayNight()
    {
        isDay = !isDay;
        ddn.ChangeSky(isDay);
        dnLight.ChangeLight(isDay);
    }
}
