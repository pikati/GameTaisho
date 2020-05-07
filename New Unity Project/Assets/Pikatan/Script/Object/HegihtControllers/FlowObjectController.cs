using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowObjectController : ObjectHeightController
{
    private bool isCollisionStage = false;
    private bool isCollisionStageEdge = false;
    private bool isCollisionStageUp = false;
    private bool isCollisionStageDown = false;
    private float angle = 0;
    private Vector3 collisionPosition;
    private DayNightChanger dnChanger;
    private GameStateController gameCtrl;
    private bool oldIsDay;  //前フレームが昼か夜かを記憶しておく（昼夜を切り替えたときに足場とステージをほんの少し遠ざけるため）

    private float speed;
    private FlowDir flowDir;
    private void Start()
    {
        Init();
        collisionPosition = new Vector3(0.0f, 0.0f, 0.0f);
        dnChanger = GameObject.Find("DayNightChanger").GetComponent<DayNightChanger>();
        gameCtrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        oldIsDay = true;
        flowDir = FlowDir.NON;
        speed = 1.0f;
    }

    private void Update()
    {
        if (!gameCtrl.isProgressed) return;
        UpdatePosition();
    }

    protected override void UpdatePosition()
    {
        isCollisionStage = isCollisionStageDown | isCollisionStageUp;
        float y = transform.position.y;
        //なににもぶつかっていないときは親のアップデート実行
        if (!isCollisionStage && !isCollisionStageEdge && flowDir == FlowDir.NON)
        {
            base.UpdatePosition();
        }
        //ステージにぶつかってその角度がアレなときは停止
        else if (isCollisionStage && (int)angle % 90 == 0)
        {
            MoveStop();
        }
        //ステージの側面にぶつかったときも停止
        else if (isCollisionStageEdge)
        {
            LowerSideStop();
        }
        //ステージにぶつかってそのステージが傾いていた時はいい感じに移動
        else if (isCollisionStage && (int)angle % 90 != 0)
        {
            SlideMove();
        }
        //水流にぶつかっているとき
        else if (IsEnterFlowing())
        {
            FlowingMove();
        }
        oldIsDay = dnChanger.isDay;
    }

    private void MoveStop()
    {
        //下側にぶつかったとき
        if (isCollisionStageDown)
        {
            UpperSideStop();
        }
        //上側にぶつかったとき
        if (isCollisionStageUp)
        {
            LowerSideStop();
        }
    }

    private void UpperSideStop()
    {
        transform.position = collisionPosition;
        //水の高さが自身よりも高くなったらそっちに合わせる
        if (contoller.waterHeight >= transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, contoller.waterHeight, transform.position.z);
        }
    }

    private void LowerSideStop()
    {
        transform.position = collisionPosition;
        //水の高さが自身よりも低くなったらそっちに合わせる
        if (transform.position.y >= contoller.waterHeight)
        {
            transform.position = new Vector3(transform.position.x, contoller.waterHeight, transform.position.z);
        }
    }

    private void SlideMove()
    {
        bool isDay = dnChanger.isDay;
        int n = 1;
        if (!isDay)
        {
            n = -1;
        }
        float dTime = Time.deltaTime;
        float X = Mathf.Cos(Mathf.Deg2Rad * angle) / Mathf.Sin(Mathf.Deg2Rad * angle);
        float speed = contoller.GetUpwardSpeed();
        float a = 0;
        //昼夜が切り替わったとき
        if (oldIsDay != isDay)
        {
            //昼になったら足場をほんの少し上げてステージと当たらないようにする
            if (isDay)
            {
                a = 0.05f;
            }
            //夜になったら足場をほんの少し下げてステージと当たらないようにする
            else
            {
                a = -0.05f;
            }
        }
        transform.position += new Vector3(X * speed * dTime * n, speed * dTime * n + a, 0);
    }

    private void FlowingMove()
    {
        bool isDay = dnChanger.isDay;
        Vector3 deltaMove = Vector3.zero;
        switch (flowDir)
        {
            case FlowDir.UP:
                deltaMove = new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
                break;
            case FlowDir.DOWN:
                deltaMove = new Vector3(0.0f, speed * Time.deltaTime * -1, 0.0f);
                break;
            case FlowDir.RIGHT:
                deltaMove = new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
                break;
            case FlowDir.LEFT:
                deltaMove = new Vector3(speed * Time.deltaTime * -1, 0.0f, 0.0f);
                break;
            default:
                break;
        }
        if (oldIsDay != isDay)
        {
            ResetDirection();
        }
        transform.position += deltaMove;
    }

    private bool IsEnterFlowing()
    {
        return flowDir == FlowDir.UP || flowDir == FlowDir.DOWN || flowDir == FlowDir.RIGHT || flowDir == FlowDir.LEFT;
    }

    private void ResetDirection()
    {
        flowDir = FlowDir.NON;
    }

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage") || collision.gameObject.CompareTag("Goal"))
        {
            isCollisionStage = true;
            angle = collision.gameObject.transform.rotation.eulerAngles.z;
            collisionPosition = transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage") || collision.gameObject.CompareTag("Goal"))
        {
            isCollisionStage = false;
            angle = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stage") || other.CompareTag("Goal"))
        {
            isCollisionStage = true;
            angle = other.transform.rotation.eulerAngles.z;
            collisionPosition = transform.position;
        }
        if (other.CompareTag("StageEdge"))
        {
            collisionPosition = transform.position;
            isCollisionStageEdge = true;
        }
        if (other.CompareTag("StageUp"))
        {
            collisionPosition = transform.position;
            angle = other.transform.rotation.eulerAngles.z;
            isCollisionStageUp = true;
        }
        if (other.CompareTag("StageDown"))
        {
            collisionPosition = transform.position;
            angle = other.transform.rotation.eulerAngles.z;
            isCollisionStageDown = true;
        }

        if(other.CompareTag("FlowUp") || other.CompareTag("FlowDown") || other.CompareTag("FlowRight") || other.CompareTag("FlowLeft"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            flowDir = fw.dir;
            speed = fw.speed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stage") || other.CompareTag("Goal"))
        {
            isCollisionStage = false;
            angle = 0;
        }
        if (other.CompareTag("StageEdge"))
        {
            isCollisionStageEdge = false;
        }
        if (other.CompareTag("StageUp"))
        {
            isCollisionStageUp = false;
            angle = 0;
        }
        if (other.CompareTag("StageDown"))
        {
            isCollisionStageDown = false;
            angle = 0;
        }
        //if (other.CompareTag("FlowUp") || other.CompareTag("FlowDown") || other.CompareTag("FlowRight") || other.CompareTag("FlowLeft"))
        //    flowDir = FlowDir.NON;
    }
    #endregion
}
