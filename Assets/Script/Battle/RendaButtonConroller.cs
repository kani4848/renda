using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RendaButtonConroller : MonoBehaviour
{
    public GameObject ballPowerGage;
    public Animator charaAnim;
    public bool pushed = false;

    private AudioSource audioSource;

    private void Update()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        audioSource.Play();

        if (!pushed)
        {
            pushed = true;
        }
        float currentGage = ballPowerGage.GetComponent<RectTransform>().sizeDelta.y;
        int plusPower = 0;

        if (currentGage < 900*0.5f)
        {
            plusPower = 100;
        }
        else if (currentGage < 900*0.75f)
        {
            plusPower = 50;
        }
        else if (currentGage < 900*0.85f)
        {
            plusPower = 25;
        }
        else if (currentGage < 900*0.9f)
        {
            plusPower = 10;
        }

        ballPowerGage.GetComponent<RectTransform>().sizeDelta += new Vector2(0,plusPower);
    }
}