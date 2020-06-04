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
    private PenguinController pc;
    private PlayerAnimationController pac;
    public bool isGameClear { get; private set; } = false;
    public bool isGameOver { get; private set; } = false;


    private void Start()
    {
        ctrl = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        player = GameObject.FindGameObjectWithTag("Player");
        endText = GameObject.Find("EndText").GetComponent<EndText>();
        pc = GameObject.Find("PenguinController").GetComponent<PenguinController>();
        pac = GameObject.Find("sirokuma").GetComponent<PlayerAnimationController>();
    }

    //private void Update()
    //{
    //    if(ctrl.waterHeight >= player.transform.position.y + PLAYER_HEGIHT)
    //    {
    //        isGameOver = true;
    //        endText.DisplayGameOver();
    //        Debug.Log("GameOver");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(pc.penguinMax == player.GetComponent<PlayerManager>().penguinNum)
            {
                GameClear();
                pac.EndWalk();
                pac.EndSwim();
                pac.Goal();
            }
            else
            {
                DisplayRemainingPenguin();
            }
        }
    }

    private void GameClear()
    {
        isGameClear = true;
        endText.DisplayGameClear();
        player.GetComponent<PlayerInputManager>().SwitchActionMap("UI");
    }

    private void DisplayRemainingPenguin()
    {

    }
}
