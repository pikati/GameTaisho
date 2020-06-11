using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    public List<Level> LevelList;

    // Start is called before the first frame update
    void Start()
    {
        Fade.FadeOut();
        FillList();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            DeleteAll();

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
        Fade.FadeIn(text);
    }
}
