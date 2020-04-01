using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//これはちゃんとできてる
public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float gravity = -4.91f;
    private Vector3 velocity;
    private bool enable;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(enable)
        {
            velocity.y += gravity * Time.deltaTime;
            Vector3 pos = transform.position;
            pos.y += velocity.y * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            velocity.y = 0;
        }
    }

    public void EnableGravity()
    {
        enable = true;
    }

    public void DisableGravity()
    {
        enable = false;
    }
}
