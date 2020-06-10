using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PoseController : MonoBehaviour
{

    private GameObject canvas;
    private PlayerInputManager pim;
    private CameraController cc;
    public bool isPose { get; private set; } = false;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("PoseMenu");
        DisablePoseMenu();
        pim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cc.isStart) return;
        if(pim.isPose)
        {
            if(canvas.activeInHierarchy)
            {
                DisablePoseMenu();
            }
            else
            {
                EnablePoseMenu();
            }
        }
    }

    public void EnablePoseMenu()
    {
        canvas.SetActive(true);
        isPose = true;
        EventSystem.current.SetSelectedGameObject(transform.Find("PoseMenu/Buttons/Retry").gameObject);
    }

    public void DisablePoseMenu()
    {
        isPose = false;
        canvas.SetActive(false);
    }
}
