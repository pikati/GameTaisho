using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectBack : MonoBehaviour
{
    private PlayerInputManager pim;
    private EventSystem e;
    // Start is called before the first frame update
    void Start()
    {
        pim = GameObject.Find("sirokuma").GetComponent<PlayerInputManager>();
        e = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pim.isPCancel);
        if(pim.isPCancel)
        {
            e.SetSelectedGameObject(null);
            Fade.FadeIn("Title");
        }
    }
}
