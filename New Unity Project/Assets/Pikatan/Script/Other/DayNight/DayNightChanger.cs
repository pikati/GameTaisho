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
    private PoseController poseCtrl;
    private CameraController cc;
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
        poseCtrl = GameObject.Find("Pose").GetComponent<PoseController>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        Fade.OnEndFade += ChangeDayNight;
        isDay = isDayTime;
        ddn.ChangeSky(isDay);
    }
    void Update()
    {
        if (!isEnable || poseCtrl.isPose || !cc.isStart) return;
        if (pManager.isChange)
        {
            FindObjectOfType<AudioManager>().PlaySound("DayNight", 0);
            FindObjectOfType<AudioManager>().PlaySound("Thema", 0);
            Fade.FadeIn(2.0f);
        }
    }

    private void ChangeDayNight()
    {
        isDay = !isDay;
        ddn.ChangeSky(isDay);
        dnLight.ChangeLight(isDay);
    }
}
