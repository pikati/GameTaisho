using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingWater : MonoBehaviour
{
    [SerializeField]
    private float flowSpeed;

    [SerializeField]
    private bool isDay;

    [SerializeField]
    private bool isNight;

    private DayNightChanger dnChanger;

    public float speed { get; private set; }
    void Start()
    {
        speed = flowSpeed;
        dnChanger = GameObject.Find("DayNightChanger").GetComponent<DayNightChanger>();
        if(isDay && isNight || !isDay && !isNight)
        {
            Debug.LogError("Please make one of isDay or isNight true.(isDayかisNightのどちらかをtrueにしてください)");
        }
    }

    private void Update()
    {
        if(isDay && dnChanger.isDay)
        {
            gameObject.SetActive(true);
        }
        else if(isDay && !dnChanger.isDay)
        {
            gameObject.SetActive(false);
        }
        else if(isNight && !dnChanger.isDay)
        {
            gameObject.SetActive(true);
        }
        else if(isNight && dnChanger.isDay)
        {
            gameObject.SetActive(false);
        }
    }
}
