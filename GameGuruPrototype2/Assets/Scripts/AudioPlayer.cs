using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    private void Start()
    {
        PlayAudio();
    }
    public void PlayAudio()
    {
        audioSource.Play();
    }
}
