using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class roll : MonoBehaviour {

    public GameObject centerpoint;
    public float rotateSpeed = 50.0f;

    // Use this for initialization
    void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {

       //Vector3.up y方向への単位ベクトルなので、x,z平面に回転する。
        transform.RotateAround(centerpoint.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);

    }
}
