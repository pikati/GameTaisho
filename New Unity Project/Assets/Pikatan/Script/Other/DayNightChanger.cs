using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DayNightChanger : MonoBehaviour
{
    public bool isDay { get; private set; } = true;
    private PlayerInputManager pManager;

    private void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
    }
    void Update()
    {
        if (pManager.isChange)
        {
            isDay = !isDay;
        }
    }
}
