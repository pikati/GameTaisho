using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class IceCounter : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    private Image image;
    private GeneratableIceCounter gc;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        gc = GameObject.Find("GeneratableIceCounter").GetComponent<GeneratableIceCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = sprites[gc.generatableIceQuantity];
    }
}
