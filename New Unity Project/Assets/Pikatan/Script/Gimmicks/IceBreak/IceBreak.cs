using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceBreak : MonoBehaviour
{
    [SerializeField]
    private int loadCapacity;
    Rigidbody[] rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = new Rigidbody[transform.childCount];
        rb = gameObject.GetComponentsInChildren<Rigidbody>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerManager>().penguinNum > loadCapacity)
            {
                BreakIce();
            }
        }
    }
}
