using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TitlePlayableDirectorManager : MonoBehaviour
{
    private PlayableDirector UIPlayableDirector;
    // Start is called before the first frame update
    void Start()
    {
        UIPlayableDirector = GameObject.Find("PenguinNoize").GetComponent<PlayableDirector>();
        UIPlayableDirector.Pause();
    }

    public void StartTimeline()
    {
        UIPlayableDirector.Resume();
    }

}
