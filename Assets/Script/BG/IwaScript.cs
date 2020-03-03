using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwaScript : MonoBehaviour
{
    private bool isCrush;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.x < 0 + 300 && !isCrush)
        {
            isCrush = true;
            gameObject.GetComponent<Animator>().SetTrigger("isCrush");
        }
    }
}
