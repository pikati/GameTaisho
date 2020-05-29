using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlow : MonoBehaviour
{
    //Rigidbody rb;
    private Rigidbody rb;
    private bool Jud;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("icex").GetComponent<Rigidbody>();
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
                Jud = false;
                return;
            }

        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    //Playerタグを持つObjectがFlyRangeのもつ
    //    //Collisionから出たとき
    //    Debug.Log("LOL");
    //    //衝突していないことを確認
    //    Jud = false;
    //    return;
    //}

}
