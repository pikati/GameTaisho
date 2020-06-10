using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlow : MonoBehaviour
{
    //Rigidbody rb;
    private Rigidbody rb;
    private bool Jud;
    private bool TrFa;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GameObject.FindGameObjectWithTag("icex").GetComponent<Rigidbody>();
        Jud = false;
        TrFa = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = GameObject.Find("ICEX(Clone)");

        if (other.tag == "icex")
        {
            Debug.Log("hit");
            //衝突してることを確認。
            Jud = true;

            if(Jud)
            {
                Destroy(obj);
                return;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = GameObject.Find("ICEX(Clone)");
        
            Debug.Log("当たってる");
            //衝突してることを確認。
            TrFa = true;

            if (TrFa)
            {
                Destroy(obj);
                return;
            }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("見つからないよ");
        TrFa = false;
        return;
    }

    private void OnTriggerExit(Collider other)
    {
        //Playerタグを持つObjectがFlyRangeのもつ
        //Collisionから出たとき
        Debug.Log("LOL");
        //衝突していないことを確認
        Jud = false;
        return;
    }

}
