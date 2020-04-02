using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndJudge : MonoBehaviour
{
    private const float PLAYER_HEGIHT = 1.0f;
    private WaterHeightController ctrl;
    private GameObject player;
    private SceneController sCtrl;
    private EndText endText;
    public bool isGameClear { get; private set; } = false;
    public bool isGameOver { get; private set; } = false;

    private void Start()
    {
        ctrl = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        player = GameObject.FindGameObjectWithTag("Player");
        endText = GameObject.Find("EndText").GetComponent<EndText>();
    }

    private void Update()
    {
        if(ctrl.waterHeight >= player.transform.position.y + PLAYER_HEGIHT)
        {
            isGameOver = true;
            endText.DisplayGameOver();
            Debug.Log("GameOver");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isGameClear = true;
            Debug.Log("Goal");
            endText.DisplayGameClear();
        }
    }
}
