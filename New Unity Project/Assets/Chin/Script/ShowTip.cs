using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTip : MonoBehaviour
{
    public GameObject tip;

    public int sec = 0;

    public Animator tipAnime;

    private bool tipOn = false;

    void Start()
    {
        tip.SetActive(false);
    }

    void Update()
    {
        if (tipOn)
        {
            tipAnime.SetBool("IsIn", true);

            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.JoystickButton0))
            {
                tipAnime.SetBool("IsIn", false);
                tipOn = false;
                tip.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(cDown());
        }
        IEnumerator cDown()
        {
            yield return new WaitForSeconds(sec);
            tip.SetActive(true);
            tipOn = true;
        }
    }

    
}
