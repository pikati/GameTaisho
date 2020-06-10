using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHeightController : MonoBehaviour
{

    protected WaterHeightController controller;    //水面の高さコントローラー
    private Vector3 position;   //オブジェクトの座標
    protected PoseController poseCtrl;
    protected void Init()
    {
        controller = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        position = transform.position;
        poseCtrl = GameObject.Find("Pose").GetComponent<PoseController>();
    }

    protected virtual void UpdatePosition()
    {
        if (poseCtrl.isPose) return;
        position = transform.position;
        position.y = controller.waterHeight;
        transform.position = position;
    }
}
