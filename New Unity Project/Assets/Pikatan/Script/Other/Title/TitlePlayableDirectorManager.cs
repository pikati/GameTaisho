using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TitlePlayableDirectorManager : MonoBehaviour
{
    private PlayableDirector UIPlayableDirector;
    private PlayableDirector penguinPlayableDirector;
    private PlayableDirector shirokumaDirector;
    // Start is called before the first frame update
    void Start()
    {
        UIPlayableDirector = GameObject.Find("PenguinNoize").GetComponent<PlayableDirector>();
        UIPlayableDirector.Pause();
        shirokumaDirector = GameObject.Find("sirokuma").GetComponent<PlayableDirector>();
        shirokumaDirector.Pause();
        penguinPlayableDirector = GameObject.Find("BabyPenguin").GetComponent<PlayableDirector>();
        penguinPlayableDirector.Pause();
    }

    public void StartTimeline()
    {
        UIPlayableDirector.Resume();
        shirokumaDirector.Resume();
        penguinPlayableDirector.Resume();
    }

}
