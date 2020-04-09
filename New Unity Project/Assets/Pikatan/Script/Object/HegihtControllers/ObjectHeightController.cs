using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHeightController : MonoBehaviour
{

    protected WaterHeightController contoller;    //水面の高さコントローラー
    private Vector3 position;   //オブジェクトの座標

    protected void Init()
    {
        contoller = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        position = transform.position;
    }

    protected virtual void UpdatePosition()
    {
        position = transform.position;
        position.y = contoller.waterHeight;
        transform.position = position;
    }
}
