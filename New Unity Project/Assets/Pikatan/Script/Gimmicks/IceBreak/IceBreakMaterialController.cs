using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceBreakMaterialController : MonoBehaviour
{
    [SerializeField]
    private Texture texture;
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Debug.Log(renderer);
        Debug.Log(renderer.material);
        Debug.Log(renderer.material.GetTexture("_MainTex"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.lKey.isPressed)
        {
            renderer.material.shader = Shader.Find("Legacy Shaders/Diffuse");
            renderer.material.SetTexture("_MainTex", texture);
            Debug.Log(renderer.material.GetTexture("_MainTex"));
        }
    }
}
