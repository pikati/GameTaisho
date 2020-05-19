using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolWater : MonoBehaviour
{
    public Vector3 position { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        position = transform.GetChild(0).gameObject.transform.position;
    }
}
