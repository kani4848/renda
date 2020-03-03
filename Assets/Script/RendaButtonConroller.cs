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

        if (currentGage < 580)
        {
            plusPower = 100;
        }
        else if (currentGage < 890)
        {
            plusPower = 50;
        }
        else if (currentGage < 1080)
        {
            plusPower = 25;
        }
        else if (currentGage < 1200)
        {
            plusPower = 10;
        }

        ballPowerGage.GetComponent<RectTransform>().sizeDelta += new Vector2(0,plusPower);
        
        /*
        if (scoreManager.state == ScoreManagerScript.STATE.END)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */
    }
}