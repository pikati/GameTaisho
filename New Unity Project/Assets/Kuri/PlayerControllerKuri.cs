using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKuri : MonoBehaviour {

    public float speed = 15f;
    public GameObject centerpoint;
    public GameObject silyatinyan;
    public float rotatespeed = 20.0f;
    bool flag = false;

    //float moveX = 0f;　水平、垂直方向にいどうするために
    //float moveZ = 0f;　使ってた数。

    public Rigidbody rb;
    public Collision col;
    public GameObject Cube;
    public GameObject polarbear;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -25.00f, 0);//加えられてる重力
    }
	
	// Update is called once per frame
	void Update () {
        //moveX = Input.GetAxis("Horizontal") * speed;
        //moveZ = Input.GetAxis("Vertical") * speed;
        //Vector3 direction = new Vector3(moveX, 0, moveZ);

        if (Input.GetKey(KeyCode.UpArrow))
        {

            transform.position += transform.forward * 0.05f;

        }

        if (Input.GetKey(KeyCode.RightShift))
        {

            transform.position += transform.forward * 5.0f;

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

            transform.position -= transform.forward * 0.07f;

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * 0.05f;
            transform.Rotate(0, 5, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * 0.05f;
            transform.Rotate(0, -5, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
                rb.AddForce(transform.up * 800.0f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Vector3 tmp = GameObject.Find("polarbear").transform.position;
            //GameObject.Find("polarbear").transform.position = new Vector3(0f, 1f, 0f);

            //Rigidbody rigidBody = GameObject.Find("polarbear").GetComponent();
            //rigidBody.velocity = Vector3.zero;
            //rigidBody.angularVelocity = Vector3.zero;
            //rigidBody.ResetInertiaTensor();

        }

        //rb.velocity = new Vector3(moveX, 0, moveZ);
        if (flag = true)
        {
            
        }
    }

    //親子関係
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            flag = true; 
            transform.SetParent(col.transform);
        }

        if (col.gameObject.tag == "enemy")
        {
            flag = true;
            transform.SetParent(col.transform);
        }

    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            flag = false;
            transform.SetParent(null);
        }

        if (col.gameObject.tag == "enemy")
        {
                flag = false;
                transform.SetParent(null);
        }
        
    }

}
