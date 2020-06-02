using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    PlayableDirector pd;
    public bool isEventStart { get; set; } = false;
    
    // Start is called before the first frame update
    void Start()
    {
        pd = GameObject.Find("PenguinNoize").GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEventStart) return;

        if(pd.state != PlayState.Playing)
        {
            SceneManager.LoadScene("level-tutorial1");
        }
    }


}
