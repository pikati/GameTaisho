using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sealevel : MonoBehaviour
{
    private static float waterline;          //水面の高さコントローラー
    public GameObject panel;                 //水のテクスチャ 
    private static float playerline;         //playerの高さ
    private float watermin;                  //水面の下限   
    private float waterrateMAX;              //水面の下限とplayerの高さからの距離
    private float waterrateNOW;              //水面の現在の高さとplayerの高さからの距離
    private static float WaterHight;         //水のWidth,Hight
    private static float posy;               //水のposY

    // Start is called before the first frame update
    void Start()
    {
        //水面の下限を取得
        watermin = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>().GetMinHeight();
        Debug.Log(watermin);
       

    }

    // Update is called once per frame
    void Update()
    {
        //playerの高さを取得
        playerline = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().position.y;

        //水面の下限とplayerの高さの距離を取得
        waterrateMAX = playerline - watermin;
        
        //水面の高さを取得
        waterline = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>().waterHeight;

        //水面の高さとplayerの高さの距離を取得
        waterrateNOW = playerline - waterline;

        //水の縦のサイズを計算
        WaterHight = 90.0f * (1.0f-waterrateNOW / waterrateMAX);

        //サイズが100以上にならないように設定
        if (WaterHight > 100.0f)
        {
            WaterHight = 100.0f;
        }
        
        //水の座標Yを計算
        posy = 136 - (100.0f - WaterHight) / 2;

        Debug.Log(WaterHight);
        Debug.Log(posy);
       
      
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(100.0f, WaterHight);
        panel.GetComponent<RectTransform>().anchoredPosition  = new Vector2(362.0f, posy);
        
    }
}
