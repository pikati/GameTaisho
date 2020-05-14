using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolControl: MonoBehaviour
{

    public GameObject pool;                     //生成するゲームオブジェクト
    protected WaterHeightController waterline;  //水面の高さコントローラー
    bool waterisover;                           //水面がじめんを越えた時にtrueにする
    static private Vector3 pos;                 //凹みの座標
   

   

    // Start is called before the first frame update
    void Start()
    {
        waterisover = false;//falseが初期設定
       
    }



    // Update is called once per frame
    void Update()
    {
       
        //水面の高さを取得
        waterline = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        
        //凹の座標取得
        pos = this.transform.position;

       


        //水面が凹を越えた時に入る（1度のみ）
        if (pos.y+2.0f <waterline.waterHeight&&!waterisover)
        {
            //Instantiate( 生成するオブジェクト,  場所, 回転 );  回転をそのままにする
            Instantiate(pool, new Vector3(pos.x, pos.y+1.0f, 0.0f), Quaternion.identity);

           waterisover = true;//trueにして一度のみの実行に

        }
        
    }

}
