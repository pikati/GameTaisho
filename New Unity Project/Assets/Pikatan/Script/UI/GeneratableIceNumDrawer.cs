using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratableIceNumDrawer : MonoBehaviour
{

    private GeneratableIceCounter counter;
    private Text iceQuantity;
    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.Find("GeneratableIceCounter").GetComponent<GeneratableIceCounter>();
        iceQuantity = GetComponent<Text>();
        iceQuantity.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        iceQuantity.text = counter.generatableIceQuantity.ToString();
    }
}
