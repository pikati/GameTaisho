using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    private PlayerInputManager pim;
    // Start is called before the first frame update
    void Start()
    {
        pim = GameObject.Find("sirokuma").GetComponent<PlayerInputManager>();
        pim.SwitchActionMap("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if(pim.isDecide)
        {
            Fade.FadeIn("Title");
            pim.SwitchActionMap("Move");
        }
        if(pim.isCancel)
        {
            Fade.FadeIn("Title");
            pim.SwitchActionMap("Move");
        }
    }
}
