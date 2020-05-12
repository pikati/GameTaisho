using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightLighting : MonoBehaviour
{
    private Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GameObject.Find("Directional Light").GetComponent<Light>();
    }

    public void ChangeLight(bool isDay)
    {
        if(myLight == null)
        {
            myLight = GameObject.Find("Directional Light").GetComponent<Light>();
        }
        myLight.color = isDay ? new Color(1, 1, 1) : new Color(0.4f, 0.4f, 0.4f);
    }
}
