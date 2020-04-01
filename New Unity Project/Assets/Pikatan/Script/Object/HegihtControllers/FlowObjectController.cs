using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowObjectController : ObjectHeightController
{
    private bool isCollisionStage = false;
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        float y = transform.position.y;
        UpdatePosition();
        if(isCollisionStage)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            isCollisionStage = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            isCollisionStage = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stage"))
        {
            isCollisionStage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stage"))
        {
            isCollisionStage = false;
        }
    }
    #endregion
}
