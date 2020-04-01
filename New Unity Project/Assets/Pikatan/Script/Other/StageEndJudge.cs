using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndJudge : MonoBehaviour
{

    private WaterHeightController ctrl;
    private GameObject player;
    public bool isGameClear { get; private set; } = false;
    public bool isGameOver { get; private set; } = false;

    private void Start()
    {
        ctrl = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(ctrl.waterHeight >= player.transform.position.y)
        {
            isGameOver = true;
            Debug.Log("GameOver");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isGameClear = true;
            Debug.Log("Goal");
        }
    }
}
