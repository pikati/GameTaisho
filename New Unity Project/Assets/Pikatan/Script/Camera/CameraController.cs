using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private GameObject player;  //プレイヤーのオブジェクト
    private Vector3 position;   //カメラの座標
    private bool isUp;  //カメラが上昇するか
    private bool isDown;//カメラが下降するか
    private float max = 3.0f;
    private float min = -3.0f;

    private const float CAMERA_Y = 4.0f;
    private const float CAMERA_Z = -25.0f;
    private const float CAMERA_MOVE_RAITO = 5.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        position = new Vector3(0.0f, CAMERA_Y, CAMERA_Z);
        isUp = false;
        isDown = false;
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
        if (isUp)
        {
            float maxY = max + player.transform.position.y + CAMERA_Y;
            position.y = Mathf.Lerp(position.y, maxY, Time.deltaTime * CAMERA_MOVE_RAITO);
        }
        else if (isDown)
        {
            float minY = min + player.transform.position.y + CAMERA_Y;
            position.y = Mathf.Lerp(position.y, minY, Time.deltaTime * CAMERA_MOVE_RAITO);
        }
        else
        {
            position.y = Mathf.Lerp(position.y, player.transform.position.y + CAMERA_Y, Time.deltaTime * CAMERA_MOVE_RAITO);
        }

        transform.position = position;
    }

    private void InputCamera()
    {
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            isUp = true;
            isDown = false;
        }
        else if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            isUp = false;
            isDown = true;
        }
        else
        {
            isUp = false;
            isDown = false;
        }
    }
}
