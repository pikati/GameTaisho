using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{

    private GameObject stayPos;
    private Vector3 goalPos;
    private StageEndJudge sej;
    private GoalPosition gp;
    private bool isClear = false;
    // Start is called before the first frame update
    void Start()
    {
        stayPos = null;
        sej = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
        GameObject goal = GameObject.Find("Goal");
        if (goal == null) goal = GameObject.Find("FlowGoal");
        gp = goal.GetComponent<GoalPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sej.isGameClear && !isClear)
        {
            goalPos = gp.GetPosition();
            isClear = true;
        }
        if(isClear)
        {
            EndMove();
            return;
        }
        if (stayPos != null)
        {
            transform.position = stayPos.transform.position;
            transform.rotation = stayPos.transform.rotation;
        }
    }

    private void EndMove()
    {
        transform.position = Vector3.Slerp(transform.position, goalPos, Time.deltaTime * 5.0f);
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
            Destroy(GetComponent<BoxCollider>());
        }
    }
}
