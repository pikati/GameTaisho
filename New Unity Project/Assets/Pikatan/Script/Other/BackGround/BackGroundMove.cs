using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime;
    }
}
