using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 move;
    private Rigidbody rb;
    private GameObject start;//リスポーン処理に分割「
    private bool isMove = true;
    private WaterSurface waterSurface;
    private float time;//いらない
    private Vector3 targetPos;//現状いらない
    private GameObject[] enemys;//なんで持ってんのw

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waterSurface = GameObject.Find("WaterHeightController").GetComponent<WaterSurface>();
        time = 0;
        start = GameObject.Find("Start");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMove)
        {
            return;
        }
        var pos = transform.position;
        pos.x += move.x;
        transform.position = pos;

        InWater();
    }

    public void OnMove(InputValue inputValue)
    {
        if (!isMove)
        {
            return;
        }
        var t = Time.deltaTime;
        var move = inputValue.Get<Vector2>();
        this.move = move * 3.0f * t;
        //Debug.Log("time  :" + t);
        //Debug.Log("move.x:" + move.x);
        //Debug.Log("Move" + inputValue.Get<Vector2>());
    }

    public void OnFire()
    {
        Debug.Log("Fire");
    }

    public void OnJump()
    {
        if (!isMove)
        {
            return;
        }
        rb.AddForce(new Vector3(0.0f, 8.0f, 0.0f), ForceMode.Impulse);
        Debug.Log("Jump");
    }

    public void Moving(bool move)
    {
        isMove = move;
    }

    public void SetFlowObject(GameObject obj)
    {
        //Array.
    }

    public Vector3 GetTargetPosition()
    {
        return targetPos;
    }

    private void InWater()
    {
        var waterHegiht = waterSurface.GetWaterHeight();
        if(gameObject.transform.position.y <= waterHegiht)
        {
            Vector3 newPos = new Vector3(gameObject.transform.position.x, waterHegiht, gameObject.transform.position.z);
            gameObject.transform.position = newPos;
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, 0, 0), ForceMode.Acceleration);
           
                Vector3 resetPos = start.transform.position;
                resetPos.y += 0.5f;
                gameObject.transform.position = resetPos;
                time = 0;
            
            targetPos = gameObject.transform.position;
            SetEnemysTarget();
        }
        else if(gameObject.transform.position.y > waterHegiht)
        {
            rb.AddForce(new Vector3(0, -4.91f, 0), ForceMode.Acceleration);
            time = 0;
        }
    }

    private void SetEnemysTarget()
    {
        foreach(GameObject obj in enemys)
        {
            obj.GetComponent<Enemy>().SetTargetPosition(targetPos);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Vector3 resetPos = start.transform.position;
            resetPos.y += 2.0f;
            gameObject.transform.position = resetPos;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Debug.Log("ゴールしたよ");
        }
    }
}
