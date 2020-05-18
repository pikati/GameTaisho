using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundManager
{

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;

}