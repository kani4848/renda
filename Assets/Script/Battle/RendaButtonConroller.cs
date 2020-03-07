using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RendaButtonConroller : MonoBehaviour
{
    public GameObject ballPowerGage;
    public bool pushed = false;

    private void Update()
    {
        /*
        if (scoreManager.state == ScoreManagerScript.STATE.THROW)
        {
            gameObject.transform.position = new Vector3(0, -2000, 0);
        }

        if (scoreManager.state == ScoreManagerScript.STATE.END)
        {
            gameObject.transform.position = new Vector3(0, -680, 0);
            buttonText.text = "再挑戦";
        }
        */
    }

    public void OnClick()
    {

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