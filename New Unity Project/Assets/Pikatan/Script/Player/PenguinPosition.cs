using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PenguinPosition : MonoBehaviour
{
    private const int MAX = 5;
    private GameObject[] penguinPos = new GameObject[MAX];
    private bool[] isPenguin = new bool[MAX];
    private PenguinController pc;
    // Start is called before the first frame update
    void Start()
    {
        Transform t = transform.Find("PenguinsPoint");
        for (int i = 0; i < MAX; i++)
        {
            penguinPos[i] = t.GetChild(i).gameObject;
            isPenguin[i] = false;
        }
        pc = GameObject.Find("PenguinController").GetComponent<PenguinController>();
    }

    public GameObject GetPenguinPosition()
    {
        for(int i = 0; i < MAX; i++)
        {
            if(!isPenguin[i])
            {
                isPenguin[i] = true;
                FindObjectOfType<AudioManager>().PlaySound("Pick", 0);
                pc.penguinCount++;
                return penguinPos[i];
            }
        }
        return null;
    }
}
