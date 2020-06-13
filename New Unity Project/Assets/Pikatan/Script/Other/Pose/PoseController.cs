using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PoseController : MonoBehaviour
{

    private GameObject canvas;
    private PlayerInputManager pim;
    private CameraController cc;
    private float moveStick;
    private bool isMove;
    public bool isPose { get; private set; } = false;
    [SerializeField]
    private Sprite[] sprites;
    private Image[] image = new Image[3];
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("PoseMenu");
        pim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        moveStick = 0.0f;
        isMove = false;
        image[0] = transform.Find("PoseMenu/Buttons/Retry").GetComponent<Image>();
        image[1] = transform.Find("PoseMenu/Buttons/BackGame").GetComponent<Image>();
        image[2] = transform.Find("PoseMenu/Buttons/BackHome").GetComponent<Image>();
        DisablePoseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        moveStick = Input.GetAxis("Vertical");

        if (!cc.isStart) return;
        if(pim.isPose)
        {
            FindObjectOfType<AudioManager>().PlaySound("Button", 0);

            if (canvas.activeInHierarchy)
            {
                DisablePoseMenu();
            }
            else
            {
                EnablePoseMenu();
            }
        }

        if(isPose && !isMove && (moveStick != 0.0f))
        {
            isMove = true;
            FindObjectOfType<AudioManager>().PlaySound("Button", 0);
            isMove = false;
        }
    }

    public void EnablePoseMenu()
    {
        for(int i = 0; i < 3; i++)
        {
            image[i].sprite = sprites[i];
        }
        canvas.SetActive(true);
        isPose = true;
        EventSystem.current.SetSelectedGameObject(transform.Find("PoseMenu/Buttons/Retry").gameObject);
    }

    public void DisablePoseMenu()
    {
        for (int i = 0; i < 3; i++)
        {
            image[i].sprite = sprites[3];
        }
        EventSystem.current.SetSelectedGameObject(null);
        isPose = false;
        StartCoroutine(EnableCanvas());
    }

    private IEnumerator EnableCanvas()
    {
        yield return new WaitForSeconds(0.4f);
        canvas.SetActive(false);
    }
}
