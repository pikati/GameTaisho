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

    private void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
        ddn = GetComponent<DisplayDayNight>();
        dnLight = GetComponent<DayNightLighting>();
    }
    void Update()
    {
        if (pManager.isChange)
        {
            isDay = !isDay;
            ddn.ChangeSky(isDay);
            dnLight.ChangeLight(isDay);
        }
    }
}
