using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriDes : MonoBehaviour
{

    int Counter;
    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        Counter = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject OBJ = GameObject.Find("ICEX(Clone)");
        GameObject ICEX = GameObject.FindGameObjectWithTag("icex");
        if (other.tag=="icex")
        {
            Counter -= 1;
            Debug.Log(Counter);
            Destroy(OBJ);
        }
        if (Counter == 0)
        {
            transform.position = new Vector3(pos.x, pos.y, pos.z);
            Counter = 4;
        }

    }


}
