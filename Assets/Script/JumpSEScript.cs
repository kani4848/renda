using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSEScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jumpSE;
   public void PlayJumpSE()
    {
        audioSource.PlayOneShot(jumpSE);
    }
}
