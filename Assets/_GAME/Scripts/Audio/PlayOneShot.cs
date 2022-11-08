using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField, Range(0,1)]
    private float volumeScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = audioSource == null ? GetComponent<AudioSource>() : audioSource;
    }

    public void PlayOneshot(AudioClip clip)
    {
        if (clip == null)
            return;

        audioSource.PlayOneShot(clip, volumeScale);
    }
}
