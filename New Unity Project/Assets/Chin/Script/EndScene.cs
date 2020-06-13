using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().StopSound("Main");
        FindObjectOfType<AudioManager>().StopSound("DayNight");
        FindObjectOfType<AudioManager>().StopSound("Thema");

        FindObjectOfType<AudioManager>().PlaySound("Bear",0);
        FindObjectOfType<AudioManager>().PlaySound("Penguin",5);
        FindObjectOfType<AudioManager>().PlaySound("End",2);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.JoystickButton0) || Input.GetKey(KeyCode.JoystickButton1))
        {
            FindObjectOfType<AudioManager>().StopSound("Bear");
            FindObjectOfType<AudioManager>().StopSound("Penguin");
            FindObjectOfType<AudioManager>().StopSound("End");

            FindObjectOfType<AudioManager>().PlaySound("Clear", 0);

            FindObjectOfType<AudioManager>().PlaySound("Main", 0);
            FindObjectOfType<AudioManager>().PlaySound("Sea", 0);

            SceneManager.LoadScene("Title");
        }
    }
}
