using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceBreakMaterialController : MonoBehaviour
{
    [SerializeField]
    private Material material;
    private Material defaultMaterial;
    private MeshRenderer[] renderers = new MeshRenderer[9];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 9; i++)
        {
            renderers[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
        }
        defaultMaterial = renderers[0].material;
    }

    public void ChangeMaterial()
    {
        for(int i = 0; i < 9; i++)
        {
            renderers[i].material = material;
        }
    }

    public void ResetMaterial()
    {
        for (int i = 0; i < 9; i++)
        {
            renderers[i].material = defaultMaterial;
        }
    }
}
