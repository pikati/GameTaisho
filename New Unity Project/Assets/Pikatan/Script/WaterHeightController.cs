using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeightController : MonoBehaviour
{
    #region field
    [SerializeField]
    private float startHegiht;  //初期位置
    [SerializeField]
    private float maxHeight;    //上限位置 この高さまで上がったらgameover
    [SerializeField]
    private float upwardSpeed;  //水の1秒の上昇速度
    #endregion

    #region propaty
    public float waterHeight { get; private set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        waterHeight = startHegiht;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, waterHeight, transform.position.z);
        transform.position = pos;
        waterHeight += upwardSpeed * Time.deltaTime;
    }
}
