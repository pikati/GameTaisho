using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//高さを管理するクラスと水面のクラスで別にする
public class WaterSurface : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float lineWidth;
    [SerializeField]
    private float  maxHeight;
    [SerializeField]
    private float minHeight;
    [SerializeField]
    private float startHeight;
    private float waterHeight;
    private bool up = false;
    private bool down = false;
    private LineRenderer line;
    private bool isMove;
    void Start()
    {
        LineInitialize();
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMove)
        {
            return;
        }
        WaterSurfaceUpdate();
        LineUpdate();
    }

    public void OnUp(InputValue inputValue)
    {
        if (!isMove)
        {
            return;
        }
        up = !up;
        Debug.Log("Up");
    }

    public void OnDown(InputValue inputValue)
    {
        if (!isMove)
        {
            return;
        }
        down = !down;
        Debug.Log("Down");
    }

    private void LineInitialize()
    {
        waterHeight = startHeight;
        line = gameObject.GetComponent<LineRenderer>();
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = 2;
    }

    private void LineUpdate()
    {
        line.SetPosition(0, new Vector3(-50.0f, waterHeight, 0.0f));
        line.SetPosition(1, new Vector3(50.0f, waterHeight, 0.0f));
    }

    private void WaterSurfaceUpdate()
    {
        if (up)
        {
            waterHeight += 3.0f * Time.deltaTime;
            if(waterHeight >= maxHeight)
            {
                waterHeight = maxHeight;
            }
        }
        if (down)
        {
            waterHeight += -3.0f * Time.deltaTime;
            if(waterHeight <= minHeight)
            {
                waterHeight = minHeight;
            }
        }
    }

    public float GetWaterHeight()
    {
        return waterHeight;
    }

    public void Moveig(bool move)
    {
        isMove = move;
    }
}
