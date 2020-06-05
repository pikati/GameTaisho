using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//自動でPlayerInputControllerをアタッチしてくれる
//[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerInput))]

//水面の高さ管理クラス
public class PlayerManager : MonoBehaviour
{
    private const float RIGHT_ANGLE = 90.0f;
    private const float LEFT_ANGLE = 270.0f;

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
    private WaterHeightController whc;
    private float rotationX;
    private float rotationY;
    private Vector3 startPos;
    private AudioManager am;
    private bool isInWater = false;
    private PlayerAnimationController pac;
    public int penguinNum { get; private set; } = 0;
    #endregion

    #region propaty
    public bool canMove { get; set; } = true;   //プレイヤーが動けるかどうか
    public Vector3 position { get; private set; }
    public bool isMove { get; private set; } = false;
    public bool isRight { get; private set; } = true;
    #endregion

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        pTest = GetComponent<PlayerInputTest>();
        gameCtrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        sEnd = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
        pManager = GetComponent<PlayerInputManager>();
        whc = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        pac = GetComponent<PlayerAnimationController>();
    }

    void Update()
    {
        position = transform.position;
        if (!gameCtrl.isProgressed || sEnd.isGameClear || sEnd.isGameOver)
        {
            pac.EndSwim();
            pac.EndWalk();
            rb.velocity = Vector3.zero;
            if(sEnd.isGameClear)
            {
                pac.Goal();
            }
            if (sEnd.isGameOver)
            {
                pac.Drown();
            }
            return;
        }
        CheckInWater();
        Move();
    }

    private void Move()
    {
        //moveDirection = new Vector2(pTest.moveDirection, rb.velocity.y / speed);
        moveDirection = new Vector2(pManager.direction.x, rb.velocity.y / speed);
        //入力があるとき
        if (Mathf.Abs(moveDirection.x) >= 0.05f)
        {
            isMove = true;
            //rb.AddForce(moveDirection);
            
            rb.velocity = moveDirection * speed;
            Turn();
            if(Mathf.Abs(moveDirection.x) >= 0.05f)
            {
                if (!isInWater)
                {
                    pac.StartWalk();
                }
                else
                {
                    pac.StartSwim();
                }
            }

        }
        //入力がないとき
        else
        {
            isMove = false;
            //加速度を0にして移動を止める(慣性をなくす)
            pac.EndWalk();
            pac.EndSwim();
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
        }
        if(isInWater)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.99f, rb.velocity.z);
        }
        if(sEnd.isGameOver)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Turn()
    {
        if (moveDirection.x > 0.1f)
        {
            rotationY = Mathf.Lerp(transform.localEulerAngles.y, RIGHT_ANGLE, 0.1f);
            isRight = true;
        }
        else if(moveDirection.x < -0.1f)
        {
            rotationY = Mathf.Lerp(transform.localEulerAngles.y, LEFT_ANGLE, 0.1f);
            isRight = false;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotationY, 0.0f);
    }

    private void CheckInWater()
    {
        isInWater = position.y + 2.5f < whc.waterHeight ? true : false;
    }

    #region collision
    private void OnCollisionEnter(Collision collision)
    {
        if (transform.position.y < collision.transform.position.y) return;
        if (collision.gameObject.CompareTag("MoveCol"))
        {
            transform.parent = collision.transform.parent;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MoveCol"))
        {
            transform.parent = null;
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

    private void Respawn()
    {
        transform.position = startPos;
    }

    public void AddPenguinNum()
    {
        penguinNum++;
    }
    #endregion

    #region Input
    //public void OnMove(InputValue inputValue)
    //{
    //    //moveDirection = inputValue.Get<Vector2>();
    //    Debug.Log("Move" + inputValue.Get<Vector2>());
    //}
    #endregion
}
