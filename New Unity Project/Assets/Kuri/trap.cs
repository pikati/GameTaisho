using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{

    public GameObject iceoc;
    Rigidbody rb;

    Collision col;
    public bool flag;
    float seconds;

    // Use this for initialization
    void Start()
    {
        flag = false;
        seconds = 0;
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        //iceocが衝突したオブジェクトがPolor bearだった場合
        if (collision.gameObject.tag == "player")
        {
            seconds += Time.deltaTime;//時間計測
            flag = true;//衝突してるかの判定
            Debug.Log(seconds);
            if (seconds>=1.65f)
            {
             //isKinematicをオフにする
                rb.isKinematic = false;
                
            }

        }
    }

    void OnCollisionExit(Collision col)
    {
        flag = false;
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool GetFlag()
    {
        return flag;
    }

}