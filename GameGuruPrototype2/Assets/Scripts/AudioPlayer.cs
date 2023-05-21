using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioLowPassFilter passFilter;

    private float passFilterValue;
    private void Start()
    {
        passFilterValue = passFilter.cutoffFrequency;
    }
    public void PlayAudio()
    {
        audioSource.Play();
        if (passFilter.cutoffFrequency >= 22000f)
            return;
        passFilter.cutoffFrequency += 1000f;
    }
    public void ResetAudioFrequency()
    {
        passFilter.cutoffFrequency = passFilterValue;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAudio();
        }
    }
}
