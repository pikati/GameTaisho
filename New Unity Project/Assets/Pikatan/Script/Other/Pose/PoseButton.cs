using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoseButton : MonoBehaviour
{
    private PoseController pc;

    private void Start()
    {
        pc = GameObject.Find("Pose").GetComponent<PoseController>();
    }

    public void Retry()
    {
        FindObjectOfType<AudioManager>().PlaySound("Select", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackHome()
    {
        FindObjectOfType<AudioManager>().PlaySound("Select", 0);
        SceneManager.LoadScene("Title");
    }

    public void BackGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("Select", 0);
        pc.DisablePoseMenu();
    }
}
