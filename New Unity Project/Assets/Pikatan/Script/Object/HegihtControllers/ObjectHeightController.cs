using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHeightController : MonoBehaviour
{

    protected WaterHeightController contoller;    //水面の高さコントローラー
    protected Vector3 position;   //オブジェクトの座標

    protected void Init()
    {
        contoller = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        position = transform.position;
    }

    protected void UpdatePosition()
    {
        position.y = contoller.waterHeight;
        transform.position = position;
    }
}
