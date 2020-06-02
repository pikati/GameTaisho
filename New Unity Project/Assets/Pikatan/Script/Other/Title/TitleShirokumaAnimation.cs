using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleShirokumaAnimation : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("sleep", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
