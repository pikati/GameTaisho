using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceBreak : MonoBehaviour
{
    Rigidbody[] rb;
    MeshCollider[] mc;
    private bool isColPlayer = false;
    private float countTime = 0;
    private IceBreakMaterialController ictrl;
    private GameStateController ctrl;
    private PoseController poseCtrl;
    private const float TAERU = 3.0f;
    private ParticleSystem particle;
    private bool isChange = false;
    private Transform tr;
    private Vector3 vec3;
    private int num;
    void Start()
    {
        rb = new Rigidbody[transform.childCount - 1];
        rb = gameObject.GetComponentsInChildren<Rigidbody>();
        mc = new MeshCollider[transform.childCount - 1];
        mc = gameObject.GetComponentsInChildren<MeshCollider>();
        ictrl = GetComponent<IceBreakMaterialController>();
        ctrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        poseCtrl = GameObject.Find("Pose").GetComponent<PoseController>();
        particle = transform.Find("BreakEffect").GetComponent<ParticleSystem>();
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            mc[i].enabled = false;
        }
        tr = transform;
        num = 0;
    }

    private void Update()
    {
        BreakIce();
    }

    private void FixedUpdate()
    {

        if (num == 0)
        {
            vec3 = tr.position;
        }

        if (countTime < 2.95f)
        {
            if (num % 2 == 0)
            {
              // float x=tr.localEulerAngles.z;


                tr.position = new Vector3(tr.position.x + countTime / 3.0f*Mathf.Cos(anglechenge(tr.localEulerAngles.z)),
                    tr.position.y + countTime / 3.0f * Mathf.Sin(anglechenge(tr.localEulerAngles.z)),
                    tr.position.z);
                num++;
            }
            else
            {
                tr.position = new Vector3(tr.position.x - countTime / 3.0f * Mathf.Cos(anglechenge(tr.localEulerAngles.z)),
                    tr.position.y - countTime / 3.0f * Mathf.Sin(anglechenge(tr.localEulerAngles.z)), tr.position.z);
                num++;
            }
        }
        else if (countTime >= 2.95f)
        {
            tr.position = vec3;
        }

    }

    private void BreakIce()
    {


        if (!isColPlayer) return;
        if (!ctrl.isProgressed) return;
        if (poseCtrl.isPose) return;

        countTime += Time.deltaTime;



        if (countTime > TAERU)
        {
            FindObjectOfType<AudioManager>().PlaySound("IceBreak", 0);

            for (int i = 0; i < transform.childCount - 1; i++)
            {
                mc[i].enabled = true;
            }
            foreach (Rigidbody r in rb)
            {
                r.isKinematic = false;
                r.transform.SetParent(null);
                Destroy(r.gameObject, 2.0f);
            }

            Destroy(gameObject);
        }
        if (!isChange)
        {
            ictrl.ChangeMaterial();
            isChange = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            particle.Play();
            isColPlayer = true;
            Debug.Log("as");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColPlayer = false;
            particle.Stop();
        }
    }

    private float anglechenge(float angle)
    {
        float rad = angle * Mathf.PI / 180.0f;
        return rad;
    }
}
