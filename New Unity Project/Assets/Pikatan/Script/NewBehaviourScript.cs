using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private StageEndJudge sej;

    void Start()
    {
        sej = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
        bool b = sej.isGameClear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
