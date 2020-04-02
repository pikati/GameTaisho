using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndText : MonoBehaviour
{
    private GameObject gameClear;
    private GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameClear = transform.Find("GameClear").gameObject;
        gameOver = transform.Find("GameOver").gameObject;
        gameClear.SetActive(false);
        gameOver.SetActive(false);
    }

    public void DisplayGameClear()
    {
        gameClear.SetActive(true);
    }

    public void HideGameClear()
    {
        gameClear.SetActive(false);
    }

    public void DisplayGameOver()
    {
        gameOver.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOver.SetActive(false);
    }
}
