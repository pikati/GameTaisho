using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePike : MonoBehaviour
{

    float Clock;//時間計測
    public Vector3 Vec;
    public float Tick;
    public GameObject icexPrefab;//
    //public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Clock = 0.0f;//pop1
        
    }

    // Update is called once per frame
    void Update()
    {
        //一つ目のPOP
        Clock += Time.deltaTime;//時間計測
        if (Clock>=Tick)
        {//72610
            GameObject ICEX = Instantiate(icexPrefab);
            ICEX.transform.position = new Vector3(Vec.x, Vec.y, Vec.z);
            //経過時間を初期化して再度時間計測を始める
            Clock = 0.0f;
        }

    }
 
}
