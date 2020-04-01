using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//これもいらない

public class Enemy : MonoBehaviour
{

    private Vector3 targetPosition;
    private PlayerController PlayerController;
    private float step;
    [SerializeField]
    private float speed;
    private WaterSurface waterSurface;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        targetPosition = gameObject.transform.position;
        step = 0;
        waterSurface = GameObject.Find("WaterHeightController").GetComponent<WaterSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        var height = waterSurface.GetWaterHeight();
        if(transform.position.y >= height)
        {
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    public void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
    }
}
