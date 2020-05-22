using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Co : MonoBehaviour
{
    private const float MOVE_V = 0.01f;
    private VolumTest vt;
    private Rigidbody rb;
    private float bouyancy = 0;
    private const float WEHIGHT = 0.5235988f * 0.9f;


    private Vector3 forcez;
    // Start is called before the first frame update
    void Start()
    {
        vt = GameObject.FindGameObjectWithTag("Sea").GetComponent<VolumTest>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.upArrowKey.isPressed)
        {
            transform.position += new Vector3(0.0f, MOVE_V, 0.0f);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.position += new Vector3(0.0f, -MOVE_V, 0.0f);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position += new Vector3(-MOVE_V, 0.0f, 0.0f);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += new Vector3(MOVE_V, 0.0f, 0.0f);
        }
        CalcBouyancy();
        
    }

    private void FixedUpdate()
    {
        Vector3 force = Vector3.zero;
        force += Physics.gravity * WEHIGHT;
        forcez = Physics.gravity * WEHIGHT;
        force += new Vector3(0.0f, bouyancy, 0.0f);
        rb.AddForce(force);
    }

    private void CalcBouyancy()
    {
        bouyancy = vt.volume * -Physics.gravity.y;
        Debug.Log(bouyancy);
        Debug.Log("floce:" + forcez.y);
    }

    void OnTriggerStay(Collider col)
    {


        //if ((waterPlane.transform.position.y - (col.transform.position.y - col.transform.localScale.y / 2)) < col.transform.localScale.y) 
        //{
        //    BoxS = col.transform.localScale.x * (waterPlane.transform.position.y - (col.transform.position.y - col.transform.localScale.y / 2)) * col.transform.localScale.z; 
        //}
        //if (((waterPlane.transform.position.y - wprb.size.y) - (col.transform.position.y - col.transform.localScale.y / 2)) >= col.transform.localScale.y)
        //{
        //    BoxS = col.transform.localScale.x * col.transform.localScale.y * col.transform.localScale.z;
        //}
        //else { }
        //FP = BoxS * 98;
        //rb.AddForce(0, 1 * (float)FP, 0);
    }
}
