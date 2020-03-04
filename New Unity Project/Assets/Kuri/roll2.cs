using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roll2 : MonoBehaviour {

    public GameObject silyatinyan;
    public float rotateSpeed = 50.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Vector3.up y方向への単位ベクトルなので、x,z平面に回転する。
        transform.RotateAround(silyatinyan.transform.position, Vector3.right, rotateSpeed * Time.deltaTime);

    }
}
