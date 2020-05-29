using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    enum CameraDir 
    { 
        UP,
        DOWN,
        RIGHT,
        LEFT,
        NON
    }


    private GameObject player;  //プレイヤーのオブジェクト
    private Vector3 position;   //カメラの座標
    private bool isUp;  //カメラが上昇するか
    private bool isDown;//カメラが下降するか
    private CameraDir dir;
    private float max = 5.0f;
    private float min = -5.0f;

    private const float CAMERA_Y = 4.0f;
    private const float CAMERA_Z = -25.0f;
    private const float CAMERA_MOVE_RAITO = 5.0f;
    private PlayerInputManager pManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pManager = player.GetComponent<PlayerInputManager>();
        position = new Vector3(0.0f, CAMERA_Y, CAMERA_Z);
        dir = CameraDir.NON;
    }

    void Update()
    {
        InputCamera();
        UpdatePosition();
    }

    

    private void UpdatePosition()
    {
        position.x = Mathf.Lerp(position.x, player.transform.position.x, Time.deltaTime);

        //カメラの高さ変更箇所
        if (dir == CameraDir.UP)
        {
            float maxY = max + player.transform.position.y + CAMERA_Y;
            position.y = Mathf.Lerp(position.y, maxY, Time.deltaTime * CAMERA_MOVE_RAITO);
            position.x = Mathf.Lerp(position.x, player.transform.position.x, Time.deltaTime * CAMERA_MOVE_RAITO);
        }
        else if (dir == CameraDir.DOWN)
        {
            float minY = min + player.transform.position.y + CAMERA_Y;
            position.y = Mathf.Lerp(position.y, minY, Time.deltaTime * CAMERA_MOVE_RAITO);
            position.x = Mathf.Lerp(position.x, player.transform.position.x, Time.deltaTime * CAMERA_MOVE_RAITO);
        }
        else if (dir == CameraDir.RIGHT)
        {
            float maxX = max + player.transform.position.x;
            position.x = Mathf.Lerp(position.x, maxX, Time.deltaTime * CAMERA_MOVE_RAITO);
            position.y = Mathf.Lerp(position.y, player.transform.position.y + CAMERA_Y, Time.deltaTime * CAMERA_MOVE_RAITO);
        }
        else if (dir == CameraDir.LEFT)
        {
            float minX = min + player.transform.position.x;
            position.x = Mathf.Lerp(position.x, minX, Time.deltaTime * CAMERA_MOVE_RAITO);
            position.y = Mathf.Lerp(position.y, player.transform.position.y + CAMERA_Y, Time.deltaTime * CAMERA_MOVE_RAITO);
        }
        else
        {
            position.x = Mathf.Lerp(position.x, player.transform.position.x, Time.deltaTime * CAMERA_MOVE_RAITO);
            position.y = Mathf.Lerp(position.y, player.transform.position.y + CAMERA_Y, Time.deltaTime * CAMERA_MOVE_RAITO);
        }

        transform.position = position;
    }

    private void InputCamera()
    {
        if (pManager == null) return;
        if(pManager.cameraDirection.y >= 0.3)
        {
            dir = CameraDir.UP;
        }
        else if(pManager.cameraDirection.y <= -0.3)
        {
            dir = CameraDir.DOWN;
        }
        else if(pManager.cameraDirection.x >= 0.3)
        {
            dir = CameraDir.RIGHT;
        }
        else if(pManager.cameraDirection.x <= -0.3)
        {
            dir = CameraDir.LEFT;
        }
        else
        {
            dir = CameraDir.NON;
        }
    }
}
