using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceObjectGenerator : MonoBehaviour
{
    enum IceDirection
    { 
        RIGHT,
        LEFT,
        NON
    }


    [SerializeField]
    private GameObject ice;
    [SerializeField]
    private float speed;    //1秒に動かせる距離
    private GameStateController ctrl;
    private GameObject player;
    private GameObject moveObject;
    private IceDirection dir;
    private GeneratableIceCounter gCtrl;
    private WaterHeightController wCtrl;
    private bool isCreate;
    // Start is called before the first frame update
    void Start()
    {
        ctrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        gCtrl = GameObject.Find("GeneratableIceController").GetComponent<GeneratableIceCounter>();
        wCtrl = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        player = GameObject.FindGameObjectWithTag("Player");
        dir = IceDirection.NON;
        isCreate = false;
        CheckMiss();
    }

    // Update is called once per frame
    void Update()
    {
        Input();
        Create();
        MoveIce();
    }

    private void Input()
    {
        if(Keyboard.current.spaceKey.isPressed && ctrl.isProgressed)
        {
            isCreate = true;
        }

        if (!ctrl.isProgressed)
        {
            if ((Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) && !Keyboard.current.leftShiftKey.isPressed)
            {
                dir = IceDirection.RIGHT;
            }
            else if ((Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) && !Keyboard.current.leftShiftKey.isPressed)
            {
                dir = IceDirection.LEFT;
            }
            else
            {
                dir = IceDirection.NON;
            }

            if(Keyboard.current.enterKey.isPressed)
            {
                ctrl.isProgressed = true;
                moveObject = null;
            }
        }
    }

    private void Create()
    {
        if (!isCreate) return;
        if (gCtrl.generatableIceQuantity <= 0) return;

        gCtrl.generatableIceQuantity--;
        ctrl.isProgressed = false;
        moveObject = Instantiate(ice, new Vector3(player.transform.position.x, wCtrl.waterHeight, player.transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        isCreate = false;
    }

    private void MoveIce()
    {
        if (!ctrl.isProgressed)
        {
            float moveValueX = 0;
            if (dir == IceDirection.RIGHT)
            {
                moveValueX = speed * Time.deltaTime;
            }
            else if (dir == IceDirection.LEFT)
            {
                moveValueX = speed * Time.deltaTime * -1;
            }
            moveObject.transform.position += new Vector3(moveValueX, 0, 0);
        }
    }

    private void CheckMiss()
    {
        if(speed == 0)
        {
            Debug.LogError("speed is 0");
        }
    }
}
