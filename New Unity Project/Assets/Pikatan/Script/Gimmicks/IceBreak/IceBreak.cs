using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceBreak : MonoBehaviour
{
    Rigidbody[] rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = new Rigidbody[transform.childCount];
        rb = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.bKey.isPressed)
        {
            BreakIce();
        }
    }

    private void BreakIce()
    {
        foreach(Rigidbody r in rb)
        {
            r.isKinematic = false;
            r.transform.SetParent(null);
            Destroy(r.gameObject, 2.0f);
        }
        Destroy(gameObject);
    }
}
