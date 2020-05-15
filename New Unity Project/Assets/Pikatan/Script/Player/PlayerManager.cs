using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//自動でPlayerInputControllerをアタッチしてくれる
//[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerInput))]

//水面の高さ管理クラス
public class PlayerManager : MonoBehaviour
{
    private const float RIGHT_ANGLE = 90.0f;
    private const float LEFT_ANGLE = 270.0f;
    private const float X_ANGLE_LIMIT = 30.0f;

    #region field
    [SerializeField]
    private float speed;    //速度
    private Vector2     moveDirection;  //プレイヤーの向きと移動量
    private Rigidbody   rb;             //プレイヤーにアタッチされているRigidBody
    //private PlayerInputController playerInputController;    //プレイヤーの入力管理クラス
    private PlayerInputTest pTest;
    private GameStateController gameCtrl;
    private StageEndJudge sEnd;
    private PlayerInputManager pManager;
    private float rotationX;
    private float rotationY;
    private float rx;
    private bool isRide = false;
    public Vector3 position { get; private set; }
    #endregion

    #region propaty
    public bool canMove { get; set; } = true;   //プレイヤーが動けるかどうか
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //playerInputController = GetComponent<PlayerInputController>();
        pTest = GetComponent<PlayerInputTest>();
        gameCtrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        sEnd = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
        pManager = GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        position = transform.position;
        if (!gameCtrl.isProgressed || sEnd.isGameClear || sEnd.isGameOver)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        Move();
    }

    private void Move()
    {
        //moveDirection = new Vector2(pTest.moveDirection, rb.velocity.y / speed);
        moveDirection = new Vector2(pManager.direction.x, rb.velocity.y / speed);
        //入力があるとき
        if (moveDirection.magnitude >= 0.05f)
        {
            //rb.AddForce(moveDirection);
            rb.velocity = moveDirection * speed;
            Turn();
        }
        //入力がないとき
        else
        {
            //加速度を0にして移動を止める(慣性をなくす)
            Vector3 stop = rb.velocity;
            stop.x = 0;
            rb.velocity = stop;
        }
        if(isRide)
        {
            transform.localScale = new Vector3(0.3f, 0.15f, 0.1875f);
        }
        else
        {
            transform.localScale = new Vector3(15.0f, 15.0f, 15.0f);
        }
    }

    private void Turn()
    {
        if (moveDirection.x > 0.1f)
        {
            rotationY = Mathf.Lerp(transform.localEulerAngles.y, RIGHT_ANGLE, 0.1f); 
            rx = -rotationX;
        }
        else if(moveDirection.x < -0.1f)
        {
            rotationY = Mathf.Lerp(transform.localEulerAngles.y, LEFT_ANGLE, 0.1f);
            rx = rotationX;
        }

        transform.rotation = Quaternion.Euler(rx, rotationY, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Move"))
        {
            transform.parent = collision.transform;
            isRide = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Move"))
        {
            transform.parent = null;
            isRide = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Stage"))
        {
            rotationX = other.transform.localEulerAngles.z;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Stage"))
        {
            rotationX = 0;
        }
    }
    #region Input
    //public void OnMove(InputValue inputValue)
    //{
    //    //moveDirection = inputValue.Get<Vector2>();
    //    Debug.Log("Move" + inputValue.Get<Vector2>());
    //}
    #endregion
}
