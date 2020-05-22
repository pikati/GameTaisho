using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumTest : MonoBehaviour
{
    private float h = 0.0f;
    private const float RADIUS = 0.5f;
    private const int MAX = (int)RADIUS * 2;
    private const int MIN = 0;
    private const float N1 = Mathf.PI / 6;

    public float volume { get; private set; } = 0;

    void Update()
    {
        float r = 0.5f;
        float v = N1 * h * (3 *Mathf.Pow(Mathf.Sqrt(h * (2 * r - h)), 2) + h * h);
    }

    private void OnTriggerStay(Collider other)
    {
        h = transform.position.y - (other.transform.position.y - 0.5f);
        if (h < MIN) h = MIN;
        else if (h > MAX) h = MAX;
    }
}
