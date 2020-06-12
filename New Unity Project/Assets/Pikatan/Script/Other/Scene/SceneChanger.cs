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
    private bool isSpundPlay = false;

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
        if (Keyboard.current.iKey.isPressed) Fade.FadeIn(nextSceneName);

        if (sEnd.isGameClear)
        {
            if(!isSpundPlay)
            {
                FindObjectOfType<AudioManager>().PlaySound("Clear", 0);
                StartCoroutine(SceneChangeClear());
                isSpundPlay = true;
            }
            //全ステージクリア処理？
            if (nextSceneName == "end")
            {
                GameObject obj = GameObject.Find("GameClear");
                obj.GetComponent<Text>().text = "おしまい 5秒後に終了します";
                obj.GetComponent<Text>().fontSize = 14;
                Invoke("Quit", 5);
                return;
            }
            pManager.SwitchActionMap("Player");
        }
        else if(sEnd.isGameOver)
        {
            StartCoroutine(SceneCbangeGameOver());
        }
    }

    private IEnumerator SceneChangeClear()
    {
        yield return new WaitForSeconds(3.0f);
        Fade.FadeIn(nextSceneName);
        isChange = true;
    }

    private IEnumerator SceneCbangeGameOver()
    {
        yield return new WaitForSeconds(1.0f);
        Fade.FadeIn(SceneManager.GetActiveScene().name, 0.5f);
        isChange = true;
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
