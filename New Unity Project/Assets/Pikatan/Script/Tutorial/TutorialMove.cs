using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMove : MonoBehaviour
{
    private const float MAX_SCALE = 0.5f;
    private const float MIN_SCALE = 0;

    [SerializeField]
    protected Sprite s1;
    [SerializeField]
    protected Sprite s2;
    private bool isEnter = true;
    private float t = 0;
    private float t2;
    private Transform childeTransform;
    protected SpriteRenderer sr;
    private bool is1 = true;

    // Start is called before the first frame update
    protected void Start()
    {
        childeTransform = transform.GetChild(0);
        sr = childeTransform.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = s1;
    }

    // Update is called once per frame
    void Update()
    {
        PopUpImage();
        UpdateSprite();
    }

    protected void PopUpImage()
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
        }
    }

    protected void UpdateSprite()
    {
        if (s2 == null) return;
        if(isEnter)
        {
            t2 += Time.deltaTime;
            if(t2 > 0.8f)
            {
                if (is1)
                {
                    sr.sprite = s2;
                }
                else
                {
                    sr.sprite = s1;
                }
                is1 = !is1;
                t2 = 0;
            }
        }
    }


    private void OnTriggerStay(Collider other)
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
