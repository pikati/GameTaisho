using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Co : MonoBehaviour
{
    private const float MOVE_V = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.upArrowKey.isPressed)
        {
            transform.position += new Vector3(0.0f, MOVE_V, 0.0f);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.position += new Vector3(0.0f, -MOVE_V, 0.0f);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position += new Vector3(-MOVE_V, 0.0f, 0.0f);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += new Vector3(MOVE_V, 0.0f, 0.0f);
        }
    }
}
