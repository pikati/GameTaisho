using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFlaying : MonoBehaviour
{

    //Rigidbody rb;
    private Rigidbody rb;
    public float Tok;
    public Vector3 For;
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
            if (Sec >= Tok)
            {
                Vector3 force = new Vector3(For.x, For.y, For.z);
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
