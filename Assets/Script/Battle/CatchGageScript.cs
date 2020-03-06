using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchGageScript : MonoBehaviour
{
    public bool isCatch = false;
    private GameObject ball;
    private DontDestroyPara dontDestroyPara;
    public Text catchText;
    public float preventDamage;

    public GameObject gage;
    public GameObject badZone;
    public GameObject greatZone;
    public GameObject goodZone;

    private void Start()
    {
        ball = transform.Find("ball").gameObject;
        dontDestroyPara = GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>();
        catchText = transform.Find("CatchText").gameObject.GetComponent<Text>();

        if (dontDestroyPara.isOpponent)
        {
            ball.transform.position = new Vector3(-450, ball.transform.position.y, 0);
            gage.transform.localScale = new Vector3(-1, 1, 1);
        }
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isCatch)
        {
            if (!dontDestroyPara.isOpponent)
            {
                ball.transform.Translate(-dontDestroyPara.ballSpeed, 0, 0);
                if (ball.transform.position.x < -450)
                {
                    CatchFail();
                    isCatch = true;
                }
            }
            else
            {
                ball.transform.Translate(dontDestroyPara.ballSpeed, 0, 0);
                if (ball.transform.position.x > 450)
                {
                    CatchFail();
                    isCatch = true;
                }
            }
        }
    }

    public void StopBall()
    {
        isCatch = true;
        gameObject.transform.Find("CatchButton").gameObject.SetActive(false);
    }

    public void CheckCatch()
    {
        float ballPos = ball.transform.position.x;
        float goodPos = goodZone.transform.position.x;
        float greatPos = greatZone.transform.position.x;

        if (dontDestroyPara.isOpponent)
        {
            if (ballPos >= goodPos && ballPos <= 450)
            {
                if (ballPos >= greatPos - 50 && ballPos <= greatPos + 50)
                {
                    if(ballPos == greatPos)
                    {
                        catchText.text = "Perfect!";
                    }
                    else
                    {
                        catchText.text = "Great!";
                    }
                }
                else
                {
                    catchText.text = "Good!";
                }
                preventDamage = (225 - Mathf.Abs(greatPos - ballPos))/ 225;
                Debug.Log("prevent:" + preventDamage + "greatPos:" +greatPos);
            }
            else
            {
                CatchFail();
            }
        }
        else
        {
            if (ballPos <= goodPos && ballPos >= -450)
            {
                if (ballPos <= greatPos + 50 && ballPos >= greatPos - 50)
                {
                    if (ballPos == greatPos)
                    {
                        catchText.text = "Perfect!";
                    }
                    else
                    {
                        catchText.text = "Great!";
                    }
                }
                else
                {
                    catchText.text = "Good!";
                }
                preventDamage = (225 - Mathf.Abs(greatPos - ballPos)) / 225;
                Debug.Log("prevent:" + preventDamage + "greatPos:" + greatPos);
            }
            else
            {
                CatchFail();
            }
        }
    }

    private void CatchFail()
    {
        catchText.text = "Fail!";
        preventDamage = 0;
        gage.SetActive(false);
        ball.SetActive(false);
        gameObject.transform.Find("CatchButton").gameObject.SetActive(false);
    }
}