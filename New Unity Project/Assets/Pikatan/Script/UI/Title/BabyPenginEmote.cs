using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyPenginEmote : MonoBehaviour
{
    [SerializeField]
    private Sprite i1;
    [SerializeField]
    private Sprite i2;
    private Image image;
    private float count = 0;
    private bool b = false;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = i1;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if(count > 0.25f)
        {
            count = 0;
            if(b)
            {
                image.sprite = i2;
            }
            else
            {
                image.sprite = i1;
            }
                b = !b;
        }
    }
}
