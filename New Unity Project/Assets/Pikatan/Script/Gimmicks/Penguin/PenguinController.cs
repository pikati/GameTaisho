using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class PenguinController : MonoBehaviour
{
    public int penguinMax { get; set; }
    public int penguinCount { get; set; } = 0;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        penguinMax = GameObject.FindGameObjectsWithTag("BabyPenguin").Length;
        if(penguinMax == 0)
        {
            GameObject.Find("Penguin").SetActive(false);
        }
        else
        {
            text = GameObject.Find("PenguinText").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(penguinMax != 0)
            text.text = penguinCount.ToString() + " / " + penguinMax.ToString();
    }
}
