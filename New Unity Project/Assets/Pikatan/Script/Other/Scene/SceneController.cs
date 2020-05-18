using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    private void Start()
    {
        int sID = SceneManager.GetActiveScene().buildIndex;

        Debug.Log(sID);

        PlayerPrefs.SetInt("level-" + sID, 1);
    }

    public void ChengeScene(string sceneName)
    {
        
    }
}
