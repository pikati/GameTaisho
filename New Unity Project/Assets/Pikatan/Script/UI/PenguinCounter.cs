using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguinCounter : MonoBehaviour
{
    [SerializeField]
    private Image i1;
    [SerializeField]
    private Image i2;
    [SerializeField]
    private Sprite[] sprites;
    private PenguinController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("PenguinController").GetComponent<PenguinController>();
    }

    // Update is called once per frame
    void Update()
    {
        i1.sprite = sprites[pc.penguinMax];
        i2.sprite = sprites[pc.penguinCount];
    }
}
