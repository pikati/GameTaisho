using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*これは多分流氷設置するやつだから現状は放置でおｋ*/
public class FlowIceController : MonoBehaviour
{
    private Vector2 move;
    private bool moving = false;
    private PlayerController playerController;
    private Height height;
    private WaterSurface waterSurface;
    // Start is called before the first frame update
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.Moving(true);
        height = GameObject.Find("Manager").GetComponent<Height>();
        waterSurface = GameObject.Find("WaterHeightController").GetComponent<WaterSurface>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!moving)
        {
            return;
        }
        var pos = transform.position;
        pos.x += move.x;
        pos.y += move.y;
        transform.position = pos;
    }

    public void Move()
    {
        moving = true;
        playerController.Moving(false);
        waterSurface.Moveig(false);
    }

    public void OnMove(InputValue inputValue)
    {
        if(moving)
        {
            var t = Time.deltaTime;
            var move = inputValue.Get<Vector2>();
            this.move = move * 6.0f * t;
        }
        //Debug.Log("time  :" + t);
        //Debug.Log("move.x:" + move.x);
        //Debug.Log("Move" + inputValue.Get<Vector2>());
    }

    public void OnDecide()
    {
        moving = false;
        playerController.Moving(true);
        waterSurface.Moveig(true);
        Height.SetFlowObject(gameObject);
    }
}
