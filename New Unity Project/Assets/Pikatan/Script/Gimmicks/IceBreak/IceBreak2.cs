using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreak2 : MonoBehaviour
{
    Rigidbody[] rb;
    private bool isColPlayer = false;
    [SerializeField]
    private float taeru = 0;

    private float countTime = 0;
    private int penNum = 0;
    void Start()
    {
        rb = new Rigidbody[transform.childCount];
        rb = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    private void Update()
    {
        BreakIce();
    }

    private void BreakIce()
    {
        if (!isColPlayer) return;

        if (0 >= taeru)
        {
            foreach (Rigidbody r in rb)
            {
                r.isKinematic = false;
                r.transform.SetParent(null);
                Destroy(r.gameObject, 2.0f);
            }
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            taeru -= collision.gameObject.GetComponent<PlayerManager>().penguinNum + 1;
            
            isColPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColPlayer = false;
            countTime = 0;
        }
    }
}
