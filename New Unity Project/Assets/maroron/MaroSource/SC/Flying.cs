using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Flying : MonoBehaviour
{
    //Rigidbody rb;
    private Rigidbody rb;
    //範囲に入ってるかどうかの確認
    bool flag;
    float seconds;

    void Start()
    {
        flag = false;
        seconds = 0.0f;
        //Playerのrigidbody取得
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (Keyboard.current.fKey.isPressed)
            {
                Vector3 force = new Vector3(0.0f, 11.0f, 0.0f);
                this.rb.AddForce(force, ForceMode.Impulse);
                //this.Flip(new Vector3(1.0f, 1.0f, 0));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //PlayerのタグをもつObjectのCollisionがFlyRangeのもつ
        //Collisionと衝突してる時
        if (other.tag == "Player")
        {
            Debug.Log("hit");
            seconds += Time.deltaTime;//時間計測
            Debug.Log(seconds);
            //衝突してることを確認。
            flag = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Playerタグを持つObjectがFlyRangeのもつ
        //Collisionから出たとき
        Debug.Log("LOL");
        //時間計測終了
        seconds = 0;
        //衝突していないことを確認
        flag = false;
        return;
    }

}
