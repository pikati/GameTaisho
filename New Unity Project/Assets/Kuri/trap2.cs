using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap2 : MonoBehaviour {

    Collision col;
    Rigidbody rb;
    float time;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "player")
        {
            Debug.Log("プレイヤー発見");
            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;
            pos.x = 0.0f;
            pos.y += 1.0f;
            pos.z = 0.0f;    

            myTransform.position = pos;  // 座標を設定
        }
        
    }
}
