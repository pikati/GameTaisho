using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//浮かぶオブジェクトの高さを管理しているよ
//FlowObjectPreserverとか名前
public class Height : MonoBehaviour
{
    private static List<GameObject> gameObjectList;
    private float hegiht = 0;
    private bool up = false;
    private bool down = false;
    private WaterSurface waterSurface;
    // Start is called before the first frame update
    void Start()
    {
        gameObjectList = new List<GameObject>();
        GameObject[] gameObjects1;
        gameObjects1 = GameObject.FindGameObjectsWithTag("Move");
        waterSurface = GameObject.Find("WaterHeightController").GetComponent<WaterSurface>();
        foreach(GameObject obj in gameObjects1)
        {
            gameObjectList.Add(obj);
        }
        gameObjectList.Add(GameObject.FindGameObjectWithTag("Goal"));
    }

    // Update is called once per frame
    void Update()
    {
        var hegiht = waterSurface.GetWaterHeight();
        foreach (GameObject obj in gameObjectList)
        {
            if(obj.transform.position.y <= hegiht)
            {
                Vector3 newPos = obj.transform.position;
                newPos.y = hegiht;
                obj.transform.position = newPos;
                var g = obj.GetComponent<Gravity>();
                if (g)
                {
                    g.DisableGravity();
                }
            }
            else
            {
                var g = obj.GetComponent<Gravity>();
                if (g)
                {
                    g.EnableGravity();
                }
            }
        }
    }

    public static void SetFlowObject(GameObject obj)
    {
        gameObjectList.Add(obj);
    }

    
}
