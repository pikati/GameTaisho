using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    private StageEndJudge sEnd;
    private PlayerInputManager pManager;
    private bool isChange = false;

    // Start is called before the first frame update
    void Start()
    {
        sEnd = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
        pManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange) return;

        if (Keyboard.current.uKey.isPressed) SceneManager.LoadScene(nextSceneName);


        if (sEnd.isGameClear)
        {
            //全ステージクリア処理？
            if (nextSceneName == "end")
            {
                GameObject obj = GameObject.Find("GameClear");
                obj.GetComponent<Text>().text = "おしまい 5秒後に終了します";
                obj.GetComponent<Text>().fontSize = 14;
                Invoke("Quit", 5);
                return;
            }
            StartCoroutine(SceneChange(true));
            FindObjectOfType<AudioManager>().PlaySound("Clear", 0);
            SceneManager.LoadScene(nextSceneName);
            pManager.SwitchActionMap("Player");
        }
        else if(sEnd.isGameOver)
        {
            StartCoroutine(SceneChange(false));
        }
    }

    private IEnumerator SceneChange(bool b)
    {
        yield return new WaitForSeconds(3.0f);
        if (b)
        {
            Fade.FadeIn(nextSceneName);
            isChange = true;
        }
        else
        {
            Fade.FadeIn(SceneManager.GetActiveScene().name);
            isChange = true;
        }
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
