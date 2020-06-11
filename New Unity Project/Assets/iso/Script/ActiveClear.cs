using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveClear : MonoBehaviour
{
    [SerializeField] private GameObject ClearIce;
    private StageEndJudge Clear;

    // Start is called before the first frame update
    void Start()
    {
       
        Clear = GameObject.Find("StageEndJudge").GetComponent<StageEndJudge>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Clear.isGameClear)
        {
            ClearIce.SetActive(true);
        }
    }
}
