using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFlowIce : MonoBehaviour
{
    private GameObject prefab;
    [SerializeField] private int creatableAmount;

    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)Resources.Load("Prefabs/Flow");
    }

    public void OnCreate()
    {
        if(creatableAmount > 0)
        {
            creatableAmount--;
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.GetComponent<FlowIceController>().Move();
        }
    }
}
