using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float gravity = Physics.gravity.y;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y += velocity.y * Time.deltaTime;
        transform.position = pos;
    }
}
