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

        FindObjectOfType<AudioManager>().StopSound("DayNight");
        FindObjectOfType<AudioManager>().StopSound("Thema");
    }

    public void ChengeScene(string sceneName)
    {
        
    }
}
