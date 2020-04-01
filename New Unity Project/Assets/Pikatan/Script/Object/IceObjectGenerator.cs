using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceObjectGenerator : MonoBehaviour
{
    enum IceDirection
    { 
        UP,
        DOWN,
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
    // Start is called before the first frame update
    void Start()
    {
        ctrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        player = GameObject.FindGameObjectWithTag("Player");
        dir = IceDirection.NON;
    }

    // Update is called once per frame
    void Update()
    {
        Input();
        
    }

    private void Input()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            ctrl.isProgressed = false;
            moveObject = Instantiate(ice, player.transform);
        }

        if(!ctrl.isProgressed)
        {
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            {
                dir = IceDirection.UP;
                
            }
            else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                dir = IceDirection.DOWN;
            }
            else if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            {
                dir = IceDirection.RIGHT;
            }
            else if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
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

    private void MoveIce()
    {
        float moveValueX = 0;
        float moveValueY = 0;
        if (dir == IceDirection.UP)
        {
            moveValueY = speed * Time.deltaTime;
        }
        else if(dir == IceDirection.DOWN)
        {
            moveValueY = speed * Time.deltaTime * -1;
        }
        else if(dir == IceDirection.RIGHT)
        {
            moveValueX = speed * Time.deltaTime;
        }
        else if(dir == IceDirection.LEFT)
        {
            moveValueX = speed * Time.deltaTime * -1;
        }

        moveObject.transform.position += new Vector3(moveObject.transform.position.x + moveValueX, moveObject.transform.position.y + moveValueY, moveObject.transform.position.z);
    }
}
