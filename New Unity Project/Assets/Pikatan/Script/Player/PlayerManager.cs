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
    #region field
    [SerializeField]
    private float speed;    //速度
    private Vector2     moveDirection;  //プレイヤーの向きと移動量
    private Rigidbody   rb;             //プレイヤーにアタッチされているRigidBody
    //private PlayerInputController playerInputController;    //プレイヤーの入力管理クラス
    private PlayerInputTest pTest;
    private GameStateController gameCtrl;
    private StageEndJudge sEnd;
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
    }

    void Update()
    {
        
        if (!gameCtrl.isProgressed || sEnd.isGameClear || sEnd.isGameOver)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        Move();
    }

    private void Move()
    {
        moveDirection = new Vector2(pTest.moveDirection, rb.velocity.y / speed);
        //入力があるとき
        if (moveDirection.magnitude >= 0.05f)
        {
            //rb.AddForce(moveDirection);
            rb.velocity = moveDirection * speed;
        }
        //入力がないとき
        else
        {
            //加速度を0にして移動を止める(慣性をなくす)
            Vector3 stop = rb.velocity;
            stop.x = 0;
            rb.velocity = stop;
        }
    }

    #region Input
    public void OnMove(InputValue inputValue)
    {
        //moveDirection = inputValue.Get<Vector2>();
        Debug.Log("Move" + inputValue.Get<Vector2>());
    }
    #endregion
}
