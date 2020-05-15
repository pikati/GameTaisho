using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        //DeleteAll();
        FillList();
    }

    void FillList()
    {
        foreach (var level in LevelList)
        {
            GameObject newButton = Instantiate(levelButton) as GameObject;
            LevelButton button = newButton.GetComponent<LevelButton>();
            button.LevelText.text = level.LevelText;

            if(PlayerPrefs.GetInt("level-" + button.LevelText.text) == 1)
            {
                level.Unlocked = 1;
                level.IsInteractable = true;
            }

            button.unlocked = level.Unlocked;
            button.GetComponent<Button>().interactable = level.IsInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("level-" + button.LevelText.text));


            newButton.transform.SetParent(Content);
        }

        SaveAll();
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
        SceneManager.LoadScene(text);
    }
}
