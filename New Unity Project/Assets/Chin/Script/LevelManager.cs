using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string LevelText;
        public int Unlocked;
        public bool IsInteractable;
    }

    public GameObject levelButton;
    public Transform Content;

    [SerializeField]
    private Sprite sprite;

    public List<Level> LevelList;
    private bool isInit = false;
    private bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        Fade.FadeOut();
        isMove = false;
    }
    private void Update()
    {
        if(!isInit)
        {
            FillList();
            isInit = true;
        }

        if (!isMove && Gamepad.current.leftStick.x.IsActuated())
        {
            isMove = true;
            FindObjectOfType<AudioManager>().PlaySound("Button", 0);
            isMove = false;
        }
        if (!isMove && Gamepad.current.leftStick.y.IsActuated())
        {
            isMove = true;
            FindObjectOfType<AudioManager>().PlaySound("Button", 0);
            isMove = false;
        }

        if (!isMove && Gamepad.current.dpad.x.IsActuated())
        {
            isMove = true;
            FindObjectOfType<AudioManager>().PlaySound("Button", 0);
            isMove = false;
        }
        if (!isMove && Gamepad.current.dpad.y.IsActuated())
        {
            isMove = true;
            FindObjectOfType<AudioManager>().PlaySound("Button", 0);
            isMove = false;
        }

        if (Input.GetKey(KeyCode.JoystickButton1))
        {
            FindObjectOfType<AudioManager>().PlaySound("Button", 0);
        }

        if (Gamepad.current.leftTrigger.IsActuated() && Gamepad.current.rightTrigger.IsActuated() && Input.GetKey(KeyCode.JoystickButton7))
        {
            DeleteAll();
            FindObjectOfType<AudioManager>().PlaySound("Clear", 0);

            Scene loadScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(loadScene.name);
        }
    }

    void FillList()
    {
        foreach (var level in LevelList)
        {
            GameObject newButton = Instantiate(levelButton) as GameObject;
            LevelButton button = newButton.GetComponent<LevelButton>();
            Button bu = button.GetComponent<Button>();
            button.LevelText.text = level.LevelText;

            if(PlayerPrefs.GetInt("level-" + button.LevelText.text) == 1)
            {
                level.Unlocked = 1;
                level.IsInteractable = true;
            }

            button.unlocked = level.Unlocked;
            bu.interactable = level.IsInteractable;
            bu.onClick.AddListener(() => LoadLevel("level-" + button.LevelText.text));


            newButton.transform.SetParent(Content);
            bu.transition = Selectable.Transition.Animation;
            Animator animator = newButton.AddComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Pikatan/ButtonStageSelect");
            newButton.GetComponent<Image>().sprite = sprite;
        }

        SaveAll();
        GameObject b = GameObject.Find("Contant").transform.GetChild(0).gameObject;
        EventSystem.current.SetSelectedGameObject(b);
    }

    void SaveAll()
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");

        foreach (GameObject buttons in allButtons)
        {
            LevelButton button = buttons.GetComponent<LevelButton>();
            PlayerPrefs.SetInt("level-" + button.LevelText.text, button.unlocked);
        }
    }

    void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void LoadLevel(string text)
    {
        FindObjectOfType<AudioManager>().PlaySound("Select", 0);
        Fade.FadeIn(text);
    }
}
