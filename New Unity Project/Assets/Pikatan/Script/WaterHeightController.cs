using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeightController : MonoBehaviour
{
    #region field
    [SerializeField]
    private float startHegiht;  //初期位置
    [SerializeField]
    private float maxHeight;    //上限位置 
    [SerializeField]
    private float minHeight;    //下限位置
    [SerializeField]
    private float upwardSpeed;  //水の1秒の上昇速度
    private GameStateController ctrl;
    private StageEndJudge sEnd;
    private DayNightChanger dnChanger;
    private bool oldIsDay;
    #endregion

    #region propaty
    public float waterHeight { get; set; }
    #endregion

    void Start()
    {
        ctrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        sEnd = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
        dnChanger = GameObject.Find("DayNightChanger").GetComponent<DayNightChanger>();
        waterHeight = startHegiht;
    }

    void Update()
    {
        bool isDay = dnChanger.isDay;
        if (!ctrl.isProgressed || sEnd.isGameClear || sEnd.isGameOver) return;

        Vector3 pos = new Vector3(transform.position.x, waterHeight, transform.position.z);
        transform.position = pos;
        int n = isDay ? 1 : -1;   //昼なら-1夜なら1をかけて上昇下降をコントロールする
        if(isDay != oldIsDay)
        {
            if (isDay)
            {
                waterHeight += 0.5f;
            }
            else
            {
                waterHeight += -0.5f;
            }
        }
        waterHeight += upwardSpeed * Time.deltaTime * n;
        if (waterHeight >= maxHeight) waterHeight = maxHeight;
        if (minHeight >= waterHeight) waterHeight = minHeight;
        oldIsDay = isDay;
    }

    public float GetMinHeight()
    {
        return minHeight;
    }

    public float GetMaxHeight()
    {
        return maxHeight;
    }

    public float GetStartHeight()
    {
        return startHegiht;
    }

    public float GetUpwardSpeed()
    {
        return upwardSpeed;
    }
}
