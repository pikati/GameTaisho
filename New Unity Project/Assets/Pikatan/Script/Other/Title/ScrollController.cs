using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    private ScrollRect scrollRect;
    private LevelManager lm;
    private GameObject[] buttons;
    private EventSystem eventSystem;
    private bool isInit = false;
    private int minIdx = 0;
    private int maxIdx = 14;
    private float scrollPosition = 1.0f;
    private float split = 0;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        scrollRect = GetComponent<ScrollRect>();
        buttons = new GameObject[lm.LevelList.Count];
        scrollRect.verticalNormalizedPosition = scrollPosition; 
        split = 1.0f / (lm.LevelList.Count / 5 - 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInit)
        {
            Transform t = GameObject.Find("Contant").transform;
            for (int i = 0; i < lm.LevelList.Count; i++)
            {
                buttons[i] = t.GetChild(i).gameObject;
            }
            isInit = true;
        }
        CalcScrollPosition();
        float pos = Mathf.Lerp(scrollRect.verticalNormalizedPosition, scrollPosition, Time.deltaTime * 10);
        scrollRect.verticalNormalizedPosition = pos;
    }

    private void CalcScrollPosition()
    {
        for(int i = 0; i < lm.LevelList.Count; i++)
        {
            if(eventSystem.currentSelectedGameObject == buttons[i])
            {
               if(i < minIdx)
               {
                   minIdx -= 5;
                   maxIdx -= 5;
                   scrollPosition += split;
               }
               else if(i > maxIdx)
               {
                   minIdx += 5;
                   maxIdx += 5;
                   scrollPosition -= split;
               }
            }
        }
    }
}
