using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratableIceCounter : MonoBehaviour
{
    [SerializeField]
    private int iceNum;
    public int generatableIceQuantity { get; set; } //足場を作れる量

    private void Start()
    {
        generatableIceQuantity = iceNum;
    }
}
