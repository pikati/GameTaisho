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

    // Start is called before the first frame update
    void Start()
    {
        sEnd = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
        pManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.rKey.isPressed) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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
            if (pManager.isDecide)
            {
                SceneManager.LoadScene(nextSceneName);
                pManager.SwitchActionMap("Player");
            }
        }
        else if(sEnd.isGameOver)
        {
            if (pManager.isDecide)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                pManager.SwitchActionMap("Player");
            }
        }
        
        if (Keyboard.current.escapeKey.isPressed)
        {
            Quit();
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
