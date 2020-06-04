using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    private float gameOverHeight;
    private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        gameOverHeight = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>().GetMinHeight() - 10.0f;
        text = GameObject.Find("GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < gameOverHeight)
        {

        }
    }

    private void ViewGameOver()
    {

    }
}
