using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{

    public Vector3 vec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject sirokuma = GameObject.FindGameObjectWithTag("warp");

        if (other.tag == "warp")
        {
            Debug.Log("HIT");
            transform.position = new Vector3(vec.x, vec.y, vec.z);
        }
    }

}
