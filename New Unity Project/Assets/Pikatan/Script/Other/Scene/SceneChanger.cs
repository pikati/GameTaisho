using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    private StageEndJudge sEnd;
    // Start is called before the first frame update
    void Start()
    {
        sEnd = GameObject.FindGameObjectWithTag("Goal").GetComponent<StageEndJudge>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.rightCtrlKey.isPressed)
        {
            if (sEnd.isGameClear)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            if (sEnd.isGameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (Keyboard.current.escapeKey.isPressed)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
        }
    }
}
