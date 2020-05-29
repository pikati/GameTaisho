using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TitleCinemachineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blcam;

    private void Start()
    {
        blcam.SetActive(false);
    }

    public void StartPrologueEvent()
    {
        blcam.SetActive(true);
    }
}
