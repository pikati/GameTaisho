using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleImageController : MonoBehaviour
{
    public bool isStart { get; set; } = false;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(isStart)
        {
            float speed = 1.0f;
            image.color -= new Color(0, 0, 0, speed * Time.deltaTime);
            if(image.color.a < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
