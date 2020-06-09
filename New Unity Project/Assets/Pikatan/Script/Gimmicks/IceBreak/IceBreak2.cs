using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreak2 : MonoBehaviour
{
    Rigidbody[] rb;
    private bool isColPlayer = false;
    [SerializeField]
    private int taeru = 0;

    private int taerumoto = 0;
    private IceBreakMaterialController ictrl;
    private bool isChange = false;
    private GameStateController ctrl;
    private PoseController poseCtrl;
    void Start()
    {
        rb = new Rigidbody[transform.childCount];
        rb = gameObject.GetComponentsInChildren<Rigidbody>();
        ictrl = GetComponent<IceBreakMaterialController>();
        ctrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        poseCtrl = GameObject.Find("Pose").GetComponent<PoseController>();
        taerumoto = taeru;
    }

    private void Update()
    {
        BreakIce();
    }

    private void BreakIce()
    {
        if (!isColPlayer) return;
        if (!ctrl.isProgressed) return;
        if (poseCtrl.isPose) return;

        
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
        if (taerumoto / 2 >= taeru)
        {
            if (isChange) return;
            ictrl.ChangeMaterial();
            isChange = true;
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
        }
    }
}
