using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchGageScript : MonoBehaviour
{
    public bool isCatch = false;
    public bool isStart = false;

    private GameObject ball;
    private DontDestroyPara dontDestroyPara;
    public Text catchText;
    public float preventDamage;

    public GameObject gage;
    public GameObject badZone;
    public GameObject greatZone;
    public GameObject goodZone;
    public GameObject perfectZone;

    private float badPos;
    private float greatPos;
    private float goodPos;
    private float perfectPos;

    private float badWidth;
    private float greatWidth;
    private float goodWidth;
    private float perfectWidth;

    private float intervalTime;

    private void Start()
    {
        intervalTime = Time.time + 1;
        preventDamage = 1;

        ball = transform.Find("ball").gameObject;
        ball.SetActive(false);

        dontDestroyPara = GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>();
        catchText = transform.Find("CatchText").gameObject.GetComponent<Text>();

        if (dontDestroyPara.isOpponent)
        {
            ball.transform.position = new Vector3(-450, ball.transform.position.y, 0);
            gage.transform.localScale = new Vector3(-1, 1, 1);
        }
        gameObject.SetActive(false);

        badPos = badZone.transform.position.x;
        goodPos = goodZone.transform.position.x;
        greatPos = greatZone.transform.position.x;
        perfectPos = perfectZone.transform.position.x;

        badWidth = badZone.GetComponent<RectTransform>().sizeDelta.x;
        goodWidth = goodZone.GetComponent<RectTransform>().sizeDelta.x;
        greatWidth = greatZone.GetComponent<RectTransform>().sizeDelta.x;
        perfectWidth = perfectZone.GetComponent<RectTransform>().sizeDelta.x;
    }

    private void FixedUpdate()
    {
        if (Time.time > intervalTime)
        {
            ball.SetActive(true);
            isStart = true;
        }

        if (!isCatch && isStart)
        {
            if (!dontDestroyPara.isOpponent)
            {
                ball.transform.Translate(-dontDestroyPara.ballSpeed, 0, 0);
                if (ball.transform.position.x < goodPos - goodWidth / 2)
                {
                    CatchFail();
                    isCatch = true;
                }
            }
            else
            {
                ball.transform.Translate(dontDestroyPara.ballSpeed, 0, 0);
                if (ball.transform.position.x > goodPos + goodWidth / 2)
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

        if (ballPos >= goodPos - goodWidth / 2 && ballPos <= goodPos + goodWidth / 2)
        {
            if (ballPos >= greatPos - greatWidth / 2 && ballPos <= greatPos + greatWidth / 2)
            {
                if (ballPos >= perfectPos - perfectWidth / 2 && ballPos <= perfectPos + perfectWidth / 2)
                {
                    catchText.text = "Perfect!";

                    preventDamage = 0;
                }
                else
                {
                    catchText.text = "Great!";
                    //preventDamage = 1 - (225 - Mathf.Abs(greatPos - ballPos)) / 225;
                    preventDamage = 0.5f;
                }
            }
            else
            {
                catchText.text = "Good!";
            }
        }
        else
        {
            CatchFail();
        }
    }

    private void CatchFail()
    {
        catchText.text = "Fail!";
        preventDamage = -1;
        gage.SetActive(false);
        ball.SetActive(false);
        gameObject.transform.Find("CatchButton").gameObject.SetActive(false);
    }
}