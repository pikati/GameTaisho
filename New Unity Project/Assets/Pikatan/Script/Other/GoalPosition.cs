using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPosition : MonoBehaviour
{

    private Transform[] pos = new Transform[5];
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        pos[0] = transform.Find("Goal1");
        pos[1] = transform.Find("Goal2");
        pos[2] = transform.Find("Goal3");
        pos[3] = transform.Find("Goal4");
        pos[4] = transform.Find("Goal5");
    }

    public Vector3 GetPosition()
    {
        return pos[index++].position;
    }
}
