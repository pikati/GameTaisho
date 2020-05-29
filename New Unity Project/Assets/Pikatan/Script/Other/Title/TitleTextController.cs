using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextController : MonoBehaviour
{
    public bool isStart { get; set; } = false;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if(isStart)
        {
            float speed = 1.0f;
            text.color -= new Color(0, 0, 0, speed * Time.deltaTime);
            if(text.color.a < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
