using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Buoyancy))]

public class FlowObjectController : ObjectHeightController
{
    private bool isCollisionStage = false;
    private bool isCollisionStageEdge = false;
    private bool isCollisionStageUp = false;
    private bool isCollisionStageDown = false;
    private bool isCollisionPoolWater = false;
    private bool isCollisionPlayerUp = false;
    private GameObject player;
    private float angle = 0;
    private float upAngle = 0;
    private float downAngle = 0;
    private Vector3 collisionPosition;
    private Vector3 colliderPosition;
    private DayNightChanger dnChanger;
    private GameStateController gameCtrl;
    private WaterHeightController whc;
    private bool oldIsDay;  //前フレームが昼か夜かを記憶しておく（昼夜を切り替えたときに足場とステージをほんの少し遠ざけるため）

    private float speed;
    private FlowDir flowDir;
    private Vector3 velocity;

    private bool isUp = false;
    private bool isDown = false;
    private bool isLeft = false;
    private bool isRight = false;
    private bool isUpdate = true;

    private List<float> straightAngle;

    private void Start()
    {
        Init();
        collisionPosition = new Vector3(0.0f, 0.0f, 0.0f);
        velocity = new Vector3(0.0f, 0.0f, 0.0f);
        dnChanger = GameObject.Find("DayNightChanger").GetComponent<DayNightChanger>();
        gameCtrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        whc = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        poseCtrl = GameObject.Find("Pose").GetComponent<PoseController>();
        oldIsDay = true;
        flowDir = FlowDir.NON;
        speed = 1.0f;
        straightAngle = new List<float>();
    }

    private void Update()
    {
        if (!gameCtrl.isProgressed || poseCtrl.isPose) return;
        UpdatePosition();
        //Debug.Log(transform.position.x);
        //Debug.Log(flowDir);
    }

    protected override void UpdatePosition()
    {
        isCollisionStage = isCollisionStageDown | isCollisionStageUp;
        //なににもぶつかっていないときは親のアップデート実行
        if (!isCollisionStage && !isCollisionStageEdge && !isCollisionPoolWater && !isCollisionPlayerUp && flowDir == FlowDir.NON)
        {
            //Debug.Log("FlowObjUpdate");
            FlowObjUpdate();
        }
        //ステージにぶつかってその角度がアレなときは停止
        else if (isCollisionStage && angle % 90 == 0)
        {
            MoveStop();
        }
        //ステージの側面にぶつかったときも停止
        else if (isCollisionStageEdge)
        {
            //Debug.Log("StageEdgeStop");
            StageEdgeStop();
        }
        else if (isCollisionPlayerUp)
        {
            //Debug.Log("Player");
            MoveColPlayer();
        }
        //ステージにぶつかってそのステージが傾いていた時はいい感じに移動
        else if (isCollisionStage && (int)angle % 90 != 0)
        {
            //Debug.Log("SlideMove");
            SlideMove();
        }
        //へこみ水に当たったとき
        else if(isCollisionPoolWater)
        {
            //Debug.Log("Pool");
            Pool();
        }
        //水流にぶつかっているとき
        else if (IsEnterFlowing())
        {
            //Debug.Log("FlowingMove");
            FlowingMove();
        }
        
        if(oldIsDay != dnChanger.isDay)
        {
            straightAngle.Clear();
        }
        oldIsDay = dnChanger.isDay;
    }

    private void FlowObjUpdate()
    {
        //if (Mathf.Abs(transform.position.y - whc.waterHeight) <= 0.05f)
        //{
        //    velocity = Vector3.zero;
        //    transform.position.Set(transform.position.x, whc.waterHeight, transform.position.z);
        //    return;
        //}
        //if (!isUpdate)
        //{
        //    isUpdate = true;
        //    return;
        //}
        ////float time = Time.deltaTime;
        ////float t = time * b.buoyancy * 9.8f;
        ////float t2 = time * b.GetPro() * 9.8f;
        ////Vector3 upVelocity = new Vector3(0.0f, t * 0.997f, 0.0f);
        ////Vector3 downVeelocity = new Vector3(0.0f, -t2 * 0.91f, 0.0f);
        ////velocity += upVelocity + downVeelocity;
        //if(transform.position.y < whc.waterHeight)
        //{
        //    velocity.y = 6.0f;
        //    transform.position += new Vector3(0.0f, velocity.y * Time.deltaTime, 0.0f);
        //    if(transform.position.y > whc.waterHeight)
        //    {
        //        transform.position.Set(transform.position.x, whc.waterHeight, transform.position.z);
        //    }
        //}
        //else
        //{
        //    velocity.y = -6.0f;
        //    transform.position += new Vector3(0.0f, velocity.y * Time.deltaTime, 0.0f);
        //    if (transform.position.y < whc.waterHeight)
        //    {
        //        transform.position.Set(transform.position.x, whc.waterHeight, transform.position.z);
        //    }
        //}
        base.UpdatePosition();
        //transform.position += velocity * Time.deltaTime;
        isUpdate = false;

    }

    private void MoveStop()
    {
        //上側にぶつかったとき
        if (isCollisionStageUp)
        {
            //Debug.Log("MovwStopUp");
            UpperSideStop();
        }
        //下側にぶつかったとき
        if (isCollisionStageDown)
        {
            //Debug.Log("MovwStopDown");
            LowerSideStop();
        }
    }

    private void UpperSideStop()
    {
        if (isCollisionStageDown && upAngle != downAngle)
        {
            if (controller.waterHeight >= transform.position.y)
            {
                if(downAngle == 0)
                {
                    transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                }
            }
            return;
        }
        transform.position = collisionPosition;
        if(upAngle == 0)
        {
            if (flowDir == FlowDir.NON)
            {
                //水の高さが自身よりも高くなったらそっちに合わせる
                if (controller.waterHeight >= transform.position.y)
                {
                    transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                }
            }
        }
        if(upAngle == 90.0f)
        {
            transform.position += new Vector3(-0.01f, 0.0f, 0.0f);
            collisionPosition = transform.position;
        }
    }

    private void LowerSideStop()
    {
        if (isCollisionStageUp && upAngle != downAngle)
        {
            if (transform.position.y <= controller.waterHeight)
            {
                if(upAngle == 0)
                {
                    transform.position = new Vector3(transform.position.x, controller.waterHeight, transform.position.z);
                }
            }
            return;
        }
        transform.position = collisionPosition;
        if(downAngle == 0)
        {
            if (flowDir == FlowDir.DOWN)
            {
                transform.position -= new Vector3(0.0f, 0.1f, 0.0f);
                return;
            }
            if (flowDir == FlowDir.UP) return;
            //水の高さが自身よりも低くなったらそっちに合わせる
            if (transform.position.y >= controller.waterHeight)
            {
                transform.position = new Vector3(transform.position.x, controller.waterHeight, transform.position.z);
            }
        }
        if (downAngle == 90.0f)
        {
            transform.position += new Vector3(0.01f, 0.0f, 0.0f);
            collisionPosition = transform.position;
        }
    }

    private void StageEdgeStop()
    {
        //if (flowDir != FlowDir.NON) return;
        //Edgeが上なら水面が下に行くとそっちに合わせる
        if(transform.position.y < colliderPosition.y)
        {
            if (transform.position.y >= controller.waterHeight)
            {
                transform.position += new Vector3(0.0f, -0.01f, 0.0f);
            }
            else
            {
                transform.position = collisionPosition;
            }

        }
        else
        {
            if (transform.position.y < controller.waterHeight)
            {
                transform.position += new Vector3(0.0f, 0.01f, 0.0f);
            }
            else
            {
                if(isCollisionPlayerUp)
                {
                    MoveColPlayer();
                }
                else
                {
                    transform.position = collisionPosition;
                }
            }
        }
    }

    private void SlideMove()
    {
        bool isDay = dnChanger.isDay;
        int n = 1;
        float b = 0;
        if (!isDay)
        {
            n = -1;
            //if(Mathf.Abs(transform.position.y - controller.waterHeight) < 0.1f)
            //{
            //    if(isCollisionStageUp)
            //        b = -0.1f;
            //    if (isCollisionStageDown)
            //        b = 0.1f;
            //}
        }
        float dTime = Time.deltaTime;
        float X = Mathf.Cos(Mathf.Deg2Rad * angle) / Mathf.Sin(Mathf.Deg2Rad * angle);
        float speed = controller.GetUpwardSpeed();
        float a = 0;
        //昼夜が切り替わったとき
        if (oldIsDay != isDay)
        {
            //角度に応じて上下の値を変更させようあと値をもう少し大きく
            //昼になったら足場をほんの少し上げてステージと当たらないようにする
            if (isDay)
            {
                a = 0.5f;
            }
            //夜になったら足場をほんの少し下げてステージと当たらないようにする
            else
            {
                a = -0.5f;
            }
        }
        
        if(flowDir == FlowDir.NON)
        {
            transform.position += new Vector3(X * speed * dTime * n + b, speed * dTime * n + a, 0);
        }
        if (oldIsDay != isDay)
        {
            if(isCollisionStageUp)
            {
                transform.position -= new Vector3(0.6f, 0.0f, 0.0f);
            }
            else if(isCollisionStageDown)
            {
                transform.position += new Vector3(0.6f, 0.0f, 0.0f);
            }
            
        }
    }

    private void Pool()
    {
        transform.position = new Vector3(transform.position.x, collisionPosition.y, transform.position.z);
        //水の高さが自身よりも高くなったらそっちに合わせる
        if (controller.waterHeight >= transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, controller.waterHeight, transform.position.z);
        }
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
            case FlowDir.STRAIGHT:
                float angle = straightAngle[straightAngle.Count - 1];
                deltaMove = new Vector3(speed * Time.deltaTime * -Mathf.Cos(Mathf.Deg2Rad * (angle - 90.0f)), speed * Time.deltaTime * -Mathf.Sin(Mathf.Deg2Rad * (angle - 90.0f)), 0.0f);
                break;
            default:
                break;
        }
        if (oldIsDay != isDay)
        {
            ResetDir();
        }
        transform.position += deltaMove;
        if(transform.position.y > controller.waterHeight)
        {
            transform.position = new Vector3(transform.position.x, controller.waterHeight, transform.position.z);
        }
    }

    private void MoveColPlayer()
    {
        
        transform.position = new Vector3(transform.position.x, player.transform.position.y + 3.5f, transform.position.z);
    }

    private bool IsEnterFlowing()
    {
        return flowDir == FlowDir.UP || flowDir == FlowDir.DOWN || flowDir == FlowDir.RIGHT || flowDir == FlowDir.LEFT || flowDir == FlowDir.STRAIGHT;
    }

    public void ResetDir()
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
            colliderPosition = other.transform.position;
        }
        if (other.CompareTag("StageEdge"))
        {
            collisionPosition = transform.position;
            isCollisionStageEdge = true;
            colliderPosition = other.transform.position;
        }
        if (other.CompareTag("StageUp"))
        {
            collisionPosition = transform.position;
            angle = upAngle = other.transform.rotation.eulerAngles.z;
            isCollisionStageUp = true;
            colliderPosition = other.transform.position;
        }
        if (other.CompareTag("StageDown"))
        {
            collisionPosition = transform.position;
            angle = downAngle = other.transform.rotation.eulerAngles.z;
            isCollisionStageDown = true;
            colliderPosition = other.transform.position;
        }
        if (other.CompareTag("PoolWater"))
        {
            collisionPosition = other.GetComponent<PoolWater>().position;
            isCollisionPoolWater = true;
            colliderPosition = other.transform.position;
        }

        if (other.CompareTag("FlowUp") || other.CompareTag("FlowDown") || other.CompareTag("FlowRight") || other.CompareTag("FlowLeft"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            flowDir = SetDir(fw.reverse, flowDir, fw.dir);
            speed = fw.speed;
        }
        if(other.CompareTag("FlowStraight"))
        {
            StraightFlowingWater fw = other.GetComponent<StraightFlowingWater>();
            flowDir = fw.dir;
            speed = fw.speed;
            straightAngle.Add(fw.angle);
        }
        if (other.CompareTag("Player"))
        {
            if (other.transform.position.y + 3.0f < transform.position.y)
            {
                isCollisionPlayerUp = true;
                player = other.gameObject;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FlowUp"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            if (fw.reverse)
            {
                isUp = false;
                isDown = true;
            }
            else
            {
                isUp = true;
                isDown = false;
            }
        }
        if (other.CompareTag("FlowDown"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            if (fw.reverse)
            {
                isUp = true;
                isDown = false;
            }
            else
            {
                isUp = false;
                isDown = true;
            }
        }
        if (other.CompareTag("FlowLeft"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            if (fw.reverse)
            {
                isRight = true;
                isLeft = false;
            }
            else
            {
                isRight = false;
                isLeft = true;
            }
        }
        if (other.CompareTag("FlowRight"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            if (fw.reverse)
            {
                isRight = false;
                isLeft = true;
            }
            else
            {
                isRight = true;
                isLeft = false;
            }
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
            upAngle = 0;
            angle = 0;
        }
        if (other.CompareTag("StageDown"))
        {
            isCollisionStageDown = false;
            downAngle = 0;
            angle = 0;
        }
        if (other.CompareTag("PoolWater"))
        {
            isCollisionPoolWater = false;
        }

        if (other.CompareTag("FlowUp"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            if (fw.reverse) isDown = false;
            else            isUp = false;
        }
        if (other.CompareTag("FlowDown"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            if (fw.reverse) isUp = false;
            else            isDown = false;
        }
        if (other.CompareTag("FlowLeft"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            if (fw.reverse) isRight = false;
            else            isLeft = false;
        }
        if (other.CompareTag("FlowRight"))
        {
            FlowingWater fw = other.GetComponent<FlowingWater>();
            if (fw.reverse) isLeft = false;
            else            isRight = false;
        }
        if(other.CompareTag("FlowStraight"))
        {
            straightAngle.RemoveAt(0);
        }
        if (other.CompareTag("Player"))
        {
            if (other.transform.position.y < transform.position.y)
            {
                isCollisionPlayerUp = false;
                player = null;
            }
        }
        if (!isUp && !isDown && !isLeft && !isRight && straightAngle.Count == 0) flowDir = FlowDir.NON;
    }

    private FlowDir SetDir(bool reverse, FlowDir fd, FlowDir newFd)
    {
        FlowDir re = fd;
        //反転してるとき
        if(reverse)
        {
            if (fd == FlowDir.DOWN && newFd == FlowDir.LEFT)    re = fd;
            else                                                re = newFd;
            if (isUp && isLeft)     re = FlowDir.LEFT;
            if (isDown && isLeft)   re = FlowDir.DOWN;
            if (isUp && isRight)    re = FlowDir.UP;
            if (isDown && isRight)  re = FlowDir.RIGHT;
        }
        //そのままの状態のとき
        else
        {
            if (fd == FlowDir.DOWN && newFd == FlowDir.RIGHT)       re = fd;
            else if (fd == FlowDir.LEFT && newFd == FlowDir.DOWN)   re = fd;
            else if(fd == FlowDir.RIGHT && newFd == FlowDir.UP)     re = fd;
            else                                                    re = newFd;
            if (isUp && isRight)    re = FlowDir.RIGHT;
            if (isDown && isRight)  re = FlowDir.DOWN;
            if (isUp && isLeft)     re = FlowDir.UP;
            if (isDown && isLeft)   re = FlowDir.LEFT;
        }
        return re;
    }
    #endregion
}
