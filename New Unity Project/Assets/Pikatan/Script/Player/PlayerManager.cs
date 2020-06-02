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
    #endregion

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        //playerInputController = GetComponent<PlayerInputController>();
        pTest = GetComponent<PlayerInputTest>();
        gameCtrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        sEnd = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
        pManager = GetComponent<PlayerInputManager>();
        whc = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        // am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        pac = GetComponent<PlayerAnimationController>();
    }

    void Update()
    {
        position = transform.position;
        if (!gameCtrl.isProgressed || sEnd.isGameClear || sEnd.isGameOver)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        CheckInWater();
        Move();
        if (transform.position.y < whc.GetMinHeight() - 30.0f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //transform.localScale = new Vector3(70.0f, 70.0f, 70.0f);
    }

    private void Move()
    {
        //moveDirection = new Vector2(pTest.moveDirection, rb.velocity.y / speed);
        moveDirection = new Vector2(pManager.direction.x, rb.velocity.y / speed);
        //入力があるとき
        if (moveDirection.magnitude >= 0.05f)
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
            Vector3 stop = rb.velocity;
            pac.EndWalk();
            pac.EndSwim();
            rb.velocity = stop;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void Turn()
    {
        if (moveDirection.x > 0.1f)
        {
            rotationY = Mathf.Lerp(transform.localEulerAngles.y, RIGHT_ANGLE, 0.1f); 
        }
        else if(moveDirection.x < -0.1f)
        {
            rotationY = Mathf.Lerp(transform.localEulerAngles.y, LEFT_ANGLE, 0.1f);
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
        if (transform.position.y < collision.transform.position.y) return;
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
