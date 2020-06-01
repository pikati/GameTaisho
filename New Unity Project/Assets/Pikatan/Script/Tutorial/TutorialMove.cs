using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMove : MonoBehaviour
{
    private const float MAX_SCALE = 0.8f;
    private const float MIN_SCALE = 0;

    private bool isEnter = true;
    private float t = 0;
    private Transform childeTransform;
    // Start is called before the first frame update
    void Start()
    {
        childeTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        PopUpImage();
        UpDownImage();
    }

    private void PopUpImage()
    {
        if (isEnter)
        {
            Vector3 scale = Vector3.zero;
            float d = MAX_SCALE * Time.deltaTime;
            scale = new Vector3(d, d, d);
            childeTransform.localScale += scale;
            if(childeTransform.localScale.x > MAX_SCALE)
            {
                childeTransform.localScale = new Vector3(MAX_SCALE, MAX_SCALE, MAX_SCALE);
            }
        }
        else
        {
            Vector3 scale = Vector3.zero;
            float d = MAX_SCALE * Time.deltaTime;
            scale = new Vector3(d, d, d);
            childeTransform.localScale -= scale;
            if (childeTransform.localScale.x < MIN_SCALE)
            {
                childeTransform.localScale = new Vector3(MIN_SCALE, MIN_SCALE, MIN_SCALE);
            }
        }
    }

    private void UpDownImage()
    {
        if (isEnter)
        {
            t += Time.deltaTime * 2.0f;
            Vector3 pos = new Vector3(0.0f, Mathf.Sin(t) * 0.5f + 5.0f, 0.0f);
            childeTransform.position = pos;
            if (t == 10) t = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
}
