using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFlaying : MonoBehaviour
{

    //Rigidbody rb;
    private Rigidbody rb;
    //範囲に入ってるかどうかの確認
    bool FFlag;
    
    float Sec;

    // Start is called before the first frame update
    void Start()
    {
        FFlag = false;
        Sec = 0.0f;
        //Playerのrigidbody取得
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FFlag)
        {
            if (Sec >= 8.0f)
            {
                Vector3 force = new Vector3(0.0f, 1.0f, 0.0f);
                this.rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //PlayerのタグをもつObjectのCollisionがFlyRangeのもつ
        //Collisionと衝突してる時
        if (other.tag == "Player")
        {
            Debug.Log("hit");
            Sec += Time.deltaTime;//時間計測
            Debug.Log(Sec);
            //衝突してることを確認。
            FFlag = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Playerタグを持つObjectがFlyRangeのもつ
        //Collisionから出たとき
        Debug.Log("LOL");
        //時間計測終了
        Sec = 0.0f;
        //衝突していないことを確認
        FFlag = false;
        return;
    }

}
