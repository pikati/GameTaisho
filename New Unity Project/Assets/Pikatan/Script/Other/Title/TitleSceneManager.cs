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
        Fade.SetFade();
        StartCoroutine("FadeOut");
        pd = GameObject.Find("PenguinNoize").GetComponent<PlayableDirector>();
        pim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isEventStart) return;

        if(pim.isSkip)
        {
            Fade.FadeIn("level-tutorial1");
        }

        if (pd.state != PlayState.Paused)
        {
            Fade.FadeIn("level-tutorial1");
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.0f);
        Fade.FadeOut();
    }
}
