using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBackGroundIce : MonoBehaviour
{
    private GameObject[] breakIces = new GameObject[8];
    private int breakIceIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        breakIces[0] = transform.Find("ICE2_CTRL/1").gameObject;
        breakIces[1] = transform.Find("ICE2_CTRL/2").gameObject;
        breakIces[2] = transform.Find("ICE2_CTRL/3").gameObject;
        breakIces[3] = transform.Find("ICE2_CTRL/4").gameObject;
        breakIces[4] = transform.Find("ICE2_CTRL/5").gameObject;
        breakIces[5] = transform.Find("ICE2_CTRL/ICE2_CTRL.001/6").gameObject;
        breakIces[6] = transform.Find("ICE2_CTRL/7").gameObject;
        breakIces[7] = transform.Find("ICE2_CTRL/8").gameObject;
    }

    public void BreakIce()
    {
        breakIces[breakIceIndex].AddComponent<BoxCollider>();
        breakIces[breakIceIndex].AddComponent<Rigidbody>();
        breakIces[breakIceIndex].transform.parent = null;
        Destroy(breakIces[breakIceIndex].gameObject, 10.0f);
        breakIceIndex++;
        if(breakIceIndex == 8)
        {
            Destroy(gameObject);
        }
    }
}
