using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDayNight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Material day;
    [SerializeField]
    private Material night;
    void Start()
    {
        RenderSettings.skybox = day;
    }

    public void ChangeSky(bool isDay)
    {
        RenderSettings.skybox = isDay ? day : night;
    }
}
