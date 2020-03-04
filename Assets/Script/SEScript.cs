﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip SE;
    public AudioClip SE2;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }
    public void PlaySE()
    {
        audioSource.PlayOneShot(SE);
    }
    public void PlaySE2()
    {
        audioSource.PlayOneShot(SE2);
    }
}
