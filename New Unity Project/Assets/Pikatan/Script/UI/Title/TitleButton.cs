using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    private Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        //buttons = new Button[3];
        //buttons[0] = transform.GetChild(0).gameObject.GetComponent<Button>();
        //buttons[1] = transform.GetChild(1).gameObject.GetComponent<Button>();
        //buttons[2] = transform.GetChild(2).gameObject.GetComponent<Button>();
        //ColorBlock color = new ColorBlock();
        //color.normalColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        //color.selectedColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        //color.pressedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //foreach(Button b in buttons)
        //{
        //    b.colors = color;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(buttons[0].onClick);
    }
}
