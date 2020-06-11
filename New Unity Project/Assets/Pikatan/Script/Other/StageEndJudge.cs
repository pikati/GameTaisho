using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndJudge : MonoBehaviour
{
    private GameObject player;
    private SceneController sCtrl;
    private EndText endText;
    private PenguinController pc;
    private PlayerAnimationController pac;
    private float gameOverHeight;

    public bool isGameClear { get; private set; } = false;
    public bool isGameOver { get; private set; } = false;


    private void Start()
    {
        gameOverHeight = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>().GetMinHeight() -10.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        endText = GameObject.Find("EndText").GetComponent<EndText>();
        pc = GameObject.Find("PenguinController").GetComponent<PenguinController>();
        pac = GameObject.Find("sirokuma").GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        if (gameOverHeight >= player.transform.position.y)
        {
            if(!isGameOver)
            {
                GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(pc.penguinMax == player.GetComponent<PlayerManager>().penguinNum)
            {
                GameClear();
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
    }

    private void GameOver()
    {
        isGameOver = true;
        FindObjectOfType<AudioManager>().PlaySound("Dead", 1);
    }

    private void DisplayRemainingPenguin()
    {

    }
}
