using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameramieteru : MonoBehaviour
{
    [SerializeField] private GameObject helpme;//HELPME　Sprite
    [SerializeField] private GameObject canvas;//キャンバス
    private PlayerManager Pman;//player


    private int PMax;//子ペンギン最大値
    

    private float angle;//角度計算結果
    private bool[] Flag;//使用フラグ
    GameObject[] data;//インスタンス保持
    private Transform[] posData;//Babypenguin
    GameObject[] penpos;


    void Start()
    {
        PMax = GameObject.FindGameObjectsWithTag("BabyPenguin").Length;
        Pman = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        //targetRenderer = GetComponent<Renderer>();
       

        penpos = new GameObject[PMax + 1];

        penpos = GameObject.FindGameObjectsWithTag("BabyPenguin");

        data = new GameObject[PMax + 1];
        posData = new Transform[PMax + 1];
        Flag = new bool[PMax];



        for (int i = 0; i < PMax; i++)
        {
            posData[i] = penpos[i].transform;
            Flag[i] = false;
        }

      
    }

    void Update()
    {

        for (int i = 0; i < PMax; i++)
        {
            // 表示されていない場合
            if (!CameraIn(i) && !Flag[i])
            {
                //インスタンス生成
                GameObject prefab = (GameObject)Instantiate(helpme);


                data[i] = prefab;
               
                //canvasにセット
                prefab.transform.SetParent(canvas.transform, false);

                //ヒエラルキーを0番目に
                prefab.transform.SetSiblingIndex(0);
                Flag[i] = true;


                data[i].transform.localPosition = PositionChenge(i);

            }
            // 表示されている場合の処理
            else if (CameraIn(i))
            {

                if (Flag[i])
                {

                    Flag[i] = false;

                    Destroy(data[i]);
                }



            }
            else
            {
                if (data[i] != null)
                {
                    data[i].transform.localPosition = PositionChenge(i);
                }
            }
        }
    }


    private Vector3 PositionChenge(int i)
    {
        angle = Mathf.Atan2(posData[i].position.y - Pman.position.y, posData[i].position.x - Pman.position.x);
        angle = angle * 180 / Mathf.PI;

        Vector3 vector3 = new Vector3(0, 0, 0);

        if (angle <= 45.0f && angle >= -45.0f)
        {
            vector3.x = 360.0f;
            vector3.y = angle * 2.0f * 125.0f / 90.0f;
        }
        else if (angle >= 45.0f && angle <= 135.0f)
        {
            vector3.y = 125.0f;
            vector3.x = 360.0f - (angle - 45.0f) * 720.0f / 90.0f;
        }
        else if ((angle >= 135.0f && angle <= 180.0f) || (angle <= -135.0f && angle >= -180.0f))
        {
            vector3.x = -360.0f;
            if (angle >= 135.0f && angle <= 180.0f)
            {
                vector3.y = (180.0f - angle) * 2.0f * 125.0f / 90.0f;
            }
            else if (angle <= -135.0f && angle >= -180.0f)
            {
                vector3.y = (-180 - angle) * 2.0f * 125.0f / 90.0f;
            }

        }
        else if (angle <= -45.0f && angle >= -135.0f)
        {
            vector3.y = -125.0f;
            vector3.x = 360.0f + (angle + 45.0f) * 720.0f / 90.0f;
        }
        else
        {
            Debug.LogError("意図しない角度です、確認してください");
        }


        return vector3;
    }


    private bool CameraIn(int i)
    {
        if (Mathf.Abs(Pman.position.x - posData[i].position.x) > 30.0f ||
            Mathf.Abs(Pman.position.y - posData[i].position.y) > 19.5f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}


