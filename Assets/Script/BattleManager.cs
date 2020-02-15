using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private float plusPower;
    public int pushCount = 0;
    public int prePushCount = 0;
    public float ballSpeed = 0;

    public BallPowerGage ballPowerGage;
    public ButtonConroller button;
    public Animator charAnime;
    public GameObject ball;
    public BGManager bgManager;

    private float pastTime;
    private float timeLimit = 2;

    public AudioSource audioSource;
    public AudioClip throwSE;
    public AudioClip resultSE;

    public Text record;
    public enum STATE
    {
        IDLE = -1,
        CHARGE,
        THROW,
        STOP,
        END
    }
    public STATE state;


    private void Start()
    {
        state = STATE.IDLE;
        ball.SetActive(false);
    }


    private void Update()
    {
        StateTrriger();
        StateAction();
    }
    private void StateTrriger()
    {
        switch (state)
        {
            case STATE.IDLE:
                if (button.pushed && state == STATE.IDLE)
                {
                    charAnime.SetTrigger("isCharge");
                    state = STATE.CHARGE;
                    pastTime = Time.time + timeLimit;
                }
                break;

            case STATE.CHARGE:
                if (Time.time > pastTime)
                {
                    bgManager.isScroll = true;
                    state = STATE.THROW;
                    ball.SetActive(true);
                    charAnime.SetTrigger("isThrow");
                    audioSource.PlayOneShot(throwSE);
                    
                    timeLimit = ballPowerGage.GetComponent<RectTransform>().sizeDelta.y/100;
                    ballSpeed = ballPowerGage.GetComponent<RectTransform>().sizeDelta.y / 10;
                    pastTime = Time.time + timeLimit;
                }
                break;

            case STATE.THROW:
                if (ballSpeed <= 20)
                {
                    ball.GetComponent<Animator>().SetTrigger("isFall");
                    state = STATE.STOP;
                }
                break;

            case STATE.STOP:
                if (ballSpeed <=0)
                {
                    ballSpeed = 0;
                    audioSource.PlayOneShot(resultSE);
                    record.text = "記録：" + (timeLimit) ;
                    state = STATE.END;
                }
                break;
        }
    }

    private void StateAction()
    {
        switch (state)
        {
            case STATE.IDLE:
                break;

            case STATE.CHARGE:
                if (pushCount > prePushCount)
                {
                    float currentGage = ballPowerGage.GetComponent<RectTransform>().sizeDelta.y;

                    if(currentGage < 580)
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
                    prePushCount = pushCount;
                }
                else
                {
                    ballPowerGage.gageSaize.y --;
                }

                break;

            case STATE.THROW:
                if (charAnime)
                {
                    charAnime.gameObject.transform.position -= new Vector3(ballSpeed, 0, 0);
                    if (charAnime.gameObject.transform.position.x < -1080)
                    {
                        Destroy(charAnime.gameObject);
                    }
                }
                ballSpeed -= 0.1f;
                break;
            case STATE.STOP:
                ballSpeed -= 0.1f;
                break;
            case STATE.END:
                break;
        }
    }
}