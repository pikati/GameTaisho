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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackHome()
    {
        SceneManager.LoadScene("Title");
    }

    public void BackGame()
    {
        pc.DisablePoseMenu();
    }
}
