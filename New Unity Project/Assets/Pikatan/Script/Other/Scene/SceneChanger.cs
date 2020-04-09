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

        if (!sEnd.isGameClear) return;
        if(pManager.isDecide)
        {
            SceneManager.LoadScene(nextSceneName);
            pManager.SwitchActionMap("Player");
            //if (sEnd.isGameOver)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //}
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
