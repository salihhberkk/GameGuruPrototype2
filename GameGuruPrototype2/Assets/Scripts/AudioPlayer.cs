using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void PlayAudio()
    {
        audioSource.Play();
    }
}
