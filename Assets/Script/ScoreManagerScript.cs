using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManagerScript : MonoBehaviour
{
    private float plusPower;
    public int pushCount = 0;
    public int prePushCount = 0;
    public float ballSpeed = 0;

    public BallPowerGage ballPowerGage;
    public RendaButtonConroller button;
    public Animator charAnime;
    public GameObject ball;
    public BgManager bgManager;

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

        BATTLE_THROW,

        END
    }
    public STATE state;


    private void Start()
    {
        state = STATE.IDLE;
        ball.SetActive(false);
    }


    private void FixedUpdate()
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
                    if (SceneManager.GetActiveScene().name == "battle")
                    {
                        state = STATE.BATTLE_THROW;
                    }
                    else
                    {
                        bgManager.isScroll = true;
                        state = STATE.THROW;
                        timeLimit = ballPowerGage.GetComponent<RectTransform>().sizeDelta.y / 100;
                        ballSpeed = ballPowerGage.GetComponent<RectTransform>().sizeDelta.y/5;
                    }
                    ball.SetActive(true);
                    charAnime.SetTrigger("isThrow");
                    audioSource.PlayOneShot(throwSE);
                    
                    pastTime = Time.time + timeLimit;

                }
                break;

            case STATE.THROW:
                if (ballSpeed <= 35)
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

            case STATE.BATTLE_THROW:


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
                break;

            case STATE.THROW:
                ballSpeed -= 0.5f;
                break;
            case STATE.STOP:
                ballSpeed -= 0.7f;
                break;

            case STATE.BATTLE_THROW:

                break;

            case STATE.END:
                break;
        }
    }
}