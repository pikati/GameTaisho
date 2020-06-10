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
    private bool isFade = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        pd = GameObject.Find("PenguinNoize").GetComponent<PlayableDirector>();
        pim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isFade)
        {
            Fade.SetFade();
            StartCoroutine("FadeOut");
            isFade = true;
        }
        if (!isEventStart) return;
        if(pim.isSkip)
        {
            Fade.FadeIn("level-tutorial1");
        }

        if (pd.state == PlayState.Paused)
        {
            Fade.FadeIn("level-tutorial1");
            isEventStart = false;
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2.0f);
        Fade.FadeOut();
    }
}
