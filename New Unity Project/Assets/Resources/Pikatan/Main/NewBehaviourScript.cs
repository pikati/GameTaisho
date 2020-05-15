using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private PlayerManager pm;

    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        Vector3 position = pm.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
