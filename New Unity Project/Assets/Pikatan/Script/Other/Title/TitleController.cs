using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    private TitleCinemachineManager cinemachineManager;
    private TitleTextController title;
    private TitleButtonContorller buttons;
    private TitlePlayableDirectorManager pdm;
    private TitleSceneManager tsm;

    private void Start()
    {
        cinemachineManager = GameObject.Find("CineamchineManager").GetComponent<TitleCinemachineManager>();
        title = GameObject.Find("Title").GetComponent<TitleTextController>();
        buttons = GameObject.Find("Buttons").GetComponent<TitleButtonContorller>();
        pdm = GameObject.Find("PlayableDirectorManager").GetComponent<TitlePlayableDirectorManager>();
        tsm = GameObject.Find("TitleSceneManager").GetComponent<TitleSceneManager>();
    }

    private void Update()
    {
        if(Input.GetAxis("Vertical") != 0.0f)
        {
            FindObjectOfType<AudioManager>().PlaySound("Button", 0);
        }
    }

    public void StartGame()
    {
        if (Fade.isFading) return;
        cinemachineManager.StartPrologueEvent();
        title.isStart = true;
        buttons.ChangeButtonTransition();
        buttons.isStart = true;
        pdm.StartTimeline();
        tsm.isEventStart = true;

        FindObjectOfType<AudioManager>().PlaySound("Select", 0);
    }

    public void Continue()
    {
        if (Fade.isFading) return;
        //ステージセレクト画面
        FindObjectOfType<AudioManager>().PlaySound("Select", 0);
    }

    public void ExitGame()
    {
        if (Fade.isFading) return;
        FindObjectOfType<AudioManager>().PlaySound("Select", 0);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
