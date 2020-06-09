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

    private List<Vector3> penguinPosition = new List<Vector3>();
    private int index = 0;
    public bool isStart { get; private set; } = false;
    private bool isNext = false;
    private float time = 0;
    private float skipTime = 0;

    private const float CAMERA_Y = 4.0f;
    private const float CAMERA_Z = -25.0f;
    private const float CAMERA_MOVE_RAITO = 8.0f;
    private PlayerInputManager pManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pManager = player.GetComponent<PlayerInputManager>();
        position = new Vector3(0.0f, CAMERA_Y, CAMERA_Z);
        dir = CameraDir.NON;
        GameObject obj = GameObject.Find("Goal");
        penguinPosition.Add(new Vector3(obj.transform.position.x + 0.5f, obj.transform.position.y + 0.5f, -25.0f));
        GameObject[] babyPenguins = GameObject.FindGameObjectsWithTag("BabyPenguin");
        List<Vector3> bpos = new List<Vector3>();
        for(int i = 0; i < babyPenguins.Length; i++)
        {
            bpos.Add(new Vector3(babyPenguins[i].transform.position.x, babyPenguins[i].transform.position.y, -25.0f));
        }
        for(int i = 0; i < bpos.Count - 1; i++)
        {
            if(Mathf.Abs(penguinPosition[0].magnitude - bpos[i].magnitude) > Mathf.Abs(penguinPosition[0].magnitude - bpos[i + 1].magnitude))
            {
                Vector3 tmp = bpos[i];
                bpos[i] = bpos[i + 1];
                bpos[i + 1] = tmp; 
            }
        }

        for (int i = 0; i < bpos.Count; i++)
        {
            penguinPosition.Add(bpos[i]);
        }
        penguinPosition.Add(new Vector3(player.transform.position.x, player.transform.position.y + 4.0f, -25.0f));
    }

    void Update()
    {
        if (isStart)
        {
            InputCamera();
            UpdatePosition();
        }
        else
        {
            DispPenguin();
        }
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

    private void DispPenguin()
    {
        if(pManager.isSkip)
        {
            isStart = true;
        }
        Debug.Log(skipTime);
        transform.position = Vector3.Lerp(transform.position, penguinPosition[index], Time.deltaTime * 2.0f);
        if(Mathf.Abs(transform.position.magnitude - penguinPosition[index].magnitude) <= 0.5f)
        {
            isNext = true;
        }
        if(isNext)
        {
            time += Time.deltaTime;
            if (time >= 2.0f)
            {
                isNext = false;
                time = 0;
                index++;
                if(index >= penguinPosition.Count)
                {
                    isStart = true;
                }
            }
        }
    }
}
