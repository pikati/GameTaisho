using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    PlayableDirector pd;
    private PlayerInputManager pim;
    public bool isEventStart { get; set; } = false;
    
    // Start is called before the first frame update
    void Start()
    {
        pd = GameObject.Find("PenguinNoize").GetComponent<PlayableDirector>();
        pim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isEventStart) return;

        if(pim.isSkip)
        {
            SceneManager.LoadScene("level-tutorial1");
        }

        if (pd.state != PlayState.Playing)
        {
            SceneManager.LoadScene("level-tutorial1");
        }
    }


}
