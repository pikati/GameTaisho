using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{

    private GameObject stayPos;
    // Start is called before the first frame update
    void Start()
    {
        stayPos = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(stayPos != null)
        {
            transform.position = stayPos.transform.position;
            transform.rotation = stayPos.transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            stayPos = collision.gameObject.GetComponent<PenguinPosition>().GetPenguinPosition();
            if (stayPos == null)
            {
                Debug.LogError("PenguinStayPosition is null");
            }
            collision.gameObject.GetComponent<PlayerManager>().AddPenguinNum();
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
