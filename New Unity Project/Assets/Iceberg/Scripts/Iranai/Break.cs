using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//これも放置

public class Break : MonoBehaviour
{
    [SerializeField]
    private Vector3[] pos;

    [SerializeField]
    private float[] degree;

    private int cutCount;
    private Vector3 ice;
    // Start is called before the first frame update
    void Start()
    {
        cutCount = 0;
        Invoke("CutObject", 5);
        Invoke("CutObject", 10);
        Invoke("CutObject", 15);
        ice = GameObject.Find("Ice").transform.position;
        ice.y -= 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CutObject()
    {
        Vector3 nor = Vector3.zero;
        nor = CulcNormal();
        Vector3 objPosition = Vector3.zero;
        objPosition = transform.position + pos[cutCount];
        GameObject[] objs = BLINDED_AM_ME.MeshCut.Cut(gameObject, objPosition, nor, gameObject.GetComponent<Renderer>().material);

        Destroy(objs[0].GetComponent<MeshCollider>());
        int vtxNum1 = objs[0].GetComponent<MeshFilter>().mesh.vertexCount;
        int vtxNum2 = objs[1].GetComponent<MeshFilter>().mesh.vertexCount;

        objs[0].AddComponent<MeshCollider>();
        objs[1].AddComponent<MeshCollider>();

        Vector3 center = Vector3.zero;
        Vector3[] vtx = objs[0].GetComponent<MeshFilter>().mesh.vertices;
        for(int i = 0; i < vtxNum1; i++)
        {
            Vector3 pos = vtx[i] + ice;
            center += pos;
        }
        center /= vtxNum1;
        GameObject a = new GameObject("LeftCenter");
        a.tag = "Goal";
        a.transform.position = center;
        //a.AddComponent<Gravity>();
        objs[0].transform.parent = a.transform;

        center = Vector3.zero;
        vtx = objs[1].GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < vtxNum2; i++)
        {
            Vector3 pos = vtx[i] + ice;
            center += pos;
        }
        center /= vtxNum2;
        GameObject b = new GameObject("RightCenter");
        b.transform.position = center;
        b.AddComponent<Gravity>();


        objs[1].transform.parent = b.transform;
        cutCount++;
    }

    private Vector3 CulcNormal()
    {
        Vector3 nor = Vector3.zero;
        while (degree[cutCount] > 180)
        {
            degree[cutCount] -= 180;
        }

        while (degree[cutCount] < 0)
        {
            degree[cutCount] += 180;
        }

        if (degree[cutCount] < 45)
        {
            nor.x = 0.2222222222f * degree[cutCount];
            nor.y = 1.0f;
        }
        else if (45 <= degree[cutCount] && degree[cutCount] < 90)
        {
            nor.x = 1.0f;
            nor.y = 1.0f - 0.2222222222f * (degree[cutCount] - 45.0f);
        }
        else if (90 <= degree[cutCount] && degree[cutCount] < 135)
        {
            nor.x = 1.0f;
            nor.y = -0.2222222222f * (degree[cutCount] - 90.0f);
        }
        else
        {
            nor.x = 1.0f - 0.2222222222f * (degree[cutCount] - 135.0f);
            nor.y = -1.0f;
        }
        return nor;
    }
}


//本番環境のCutObject
//private void CutObject()
//{
//    Vector3 nor = Vector3.zero;
//    nor = CulcNormal();
//    Vector3 objPosition = Vector3.zero;
//    objPosition = transform.position + pos[cutCount];
//    GameObject[] objs = BLINDED_AM_ME.MeshCut.Cut(gameObject, objPosition, nor, gameObject.GetComponent<Renderer>().material);

//    Destroy(objs[0].GetComponent<BoxCollider>());
//    int vtxNum1 = objs[0].GetComponent<MeshFilter>().mesh.vertexCount;
//    int vtxNum2 = objs[1].GetComponent<MeshFilter>().mesh.vertexCount;

//    //if (vtxNum1 > vtxNum2)
//    //{
//    //    objs[0].tag = "Goal";
//    //}
//    //else
//    //{
//    //    objs[1].tag = "Goal";
//    //}

//    //GameObject n = GameObject.Find("LeftCenter");
//    //if(n)
//    //{
//    //    Destroy(n);
//    //}
//    //n = GameObject.Find("RightCenter");
//    //if (n)
//    //{
//    //    Destroy(n);
//    //}

//    objs[0].AddComponent<BoxCollider>();

//    Vector3 center = Vector3.zero;
//    Vector3[] vtx = objs[0].GetComponent<MeshFilter>().mesh.vertices;
//    for (int i = 0; i < vtxNum1; i++)
//    {
//        Vector3 pos = vtx[i] + ice;
//        center += pos;
//    }
//    center /= vtxNum1;
//    GameObject a = new GameObject("LeftCenter");
//    a.tag = "Goal";
//    a.transform.position = center;
//    a.AddComponent<Rigidbody>();
//    a.GetComponent<Rigidbody>().constraints =
//        RigidbodyConstraints.FreezePositionX |
//        RigidbodyConstraints.FreezePositionZ |
//        RigidbodyConstraints.FreezeRotationX |
//        RigidbodyConstraints.FreezeRotationY |
//        RigidbodyConstraints.FreezeRotationZ;
//    Height.SetFlowObject(a);
//    objs[0].transform.parent = a.transform;

//    center = Vector3.zero;
//    vtx = objs[1].GetComponent<MeshFilter>().mesh.vertices;
//    for (int i = 0; i < vtxNum2; i++)
//    {
//        Vector3 pos = vtx[i] + ice;
//        center += pos;
//    }
//    center /= vtxNum2;
//    GameObject b = new GameObject("RightCenter");
//    b.transform.position = center;
//    b.AddComponent<Rigidbody>();
//    objs[1].transform.parent = b.transform;
//    Height.SetFlowObject(b);
//    cutCount++;
//}