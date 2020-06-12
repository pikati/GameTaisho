using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectBack : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    private PlayerInputManager pim;
    // Start is called before the first frame update
    void Start()
    {
        pim = obj.GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pim.isCancel);
        if(pim.isCancel)
        {
            Fade.FadeIn("Title");
        }
    }
}
