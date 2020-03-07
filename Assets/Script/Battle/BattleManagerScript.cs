using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleManagerScript : MonoBehaviour
{
    //スピード変換前のパワー
    public int count = 0;
    //各基本値
    private float initDamage = 250;
    private float initBgScroll = 400;
    private float initBallSpeed = 30;

    //パワーをもとに算出するボールスピード、アニメーションにも使うので変動
    public float bgScrollSpeed;

    private GameObject playerHp;
    private GameObject opponentHp;

    //制限時間用変数
    private float pastTime;
    private float timeLimit = 1;

    //参照するGameObject群
    public Animator charaAnim;
    public RendaButtonConroller rendaButton;
    public BallPowerGage ballPowerGage;
    public CatchGageScript catchGage;
    public BgManager bgManager;
    private DontDestroyPara dontDestroyPara;

    public enum STATE
    {
        IDLE = -1,
        CHARGE,
        THROW,
        CATCH,
        CATCH_SUCSESS,
        CATCH_FAIL,
        STOP,

        END
    }
    public STATE state;

    private void Start()
    {
        dontDestroyPara = GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>();
        playerHp = GameObject.FindGameObjectWithTag("HpGage").gameObject.transform.Find("PlayerHp").gameObject;
        opponentHp = GameObject.FindGameObjectWithTag("HpGage").gameObject.transform.Find("OpponentHp").gameObject;

        bgScrollSpeed = 400;
        bgManager = GetComponent<BgManager>();
        state = STATE.IDLE;

        if (dontDestroyPara.isLanded)
        {
            charaAnim.SetTrigger("isLanded");
            state = STATE.CATCH;
            pastTime = Time.time+2;
            catchGage.gameObject.SetActive(true);
        }

        if (dontDestroyPara.isOpponent)
        {
            catchGage.gameObject.SetActive(false);
            charaAnim.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            bgManager.isOpponent = true;
        }
    }
    private void FixedUpdate()
    {
        StateTrriger();
        StateAction();
    }

    //状態切り替え時の処理
    private void StateTrriger()
    {
        switch (state)
        {
            case STATE.IDLE:
                if (!dontDestroyPara.isOpponent)
                {
                    rendaButton.transform.position = new Vector3(0, -630, 0);
                }
                else
                {
                    if(Time.time > pastTime)
                    {
                        pastTime = Time.time + timeLimit;
                        charaAnim.SetTrigger("isCharge");
                        state = STATE.CHARGE;
                    }
                }
                if (rendaButton.pushed)
                {
                    ballPowerGage.isCharge = true;
                    charaAnim.SetTrigger("isCharge");
                    state = STATE.CHARGE;
                    pastTime = Time.time + timeLimit;
                }
                break;

            case STATE.CHARGE:
                if (Time.time > pastTime)
                {
                    float chargePower;
                    //チャージゲージから背景スクロール速度とキャッチゲージ速度と基本ダメージを算出する。
                    if (!dontDestroyPara.isOpponent)
                    {
                        chargePower = ballPowerGage.GetComponent<RectTransform>().sizeDelta.y / 450;
                    }
                    else
                    {
                        chargePower = dontDestroyPara.comChargePower;
                    }
                    dontDestroyPara.ballSpeed = initBallSpeed * chargePower;
                    dontDestroyPara.damage = initDamage * chargePower;

                    ballPowerGage.isCharge = false;
                    charaAnim.SetTrigger("isThrow");
                    rendaButton.transform.position = new Vector3(0,2000,0);
                    pastTime = Time.time + timeLimit;
                    
                    state = STATE.THROW;
                }
                break;

            case STATE.THROW:
                if (!dontDestroyPara.isOpponent)
                {
                    dontDestroyPara.isOpponent = true;
                }
                else
                {
                    dontDestroyPara.isOpponent = false;
                }
                dontDestroyPara.isLanded = true;
                state = STATE.END;
                break;

            case STATE.CATCH:
                if (dontDestroyPara.isOpponent)
                {
                    if(Time.time > pastTime)
                    {
                        catchGage.isCatch = true;
                    }
                }
                if(catchGage.isCatch)
                {
                    if (!dontDestroyPara.isOpponent)
                    {
                        catchGage.CheckCatch();
                    }
                    else
                    {
                        catchGage.preventDamage = dontDestroyPara.comPrevent;
                    }

                    SEScript playSE = GetComponent<SEScript>();
                    playSE.PlaySE();
                    bgManager.isScroll = true;
                    if (catchGage.preventDamage < 0)
                    {
                        charaAnim.SetTrigger("isCatchFail");
                        pastTime = Time.time + 2;

                        Debug.Log("キャッチ失敗");
                        state = STATE.CATCH_FAIL;
                    }
                    else
                    {
                        //キャッチゲージを参照してダメージを軽減
                        dontDestroyPara.damage *= catchGage.preventDamage;
                        bgScrollSpeed = initBgScroll *(dontDestroyPara.damage/ initDamage );
                        dontDestroyPara.updateDamage = dontDestroyPara.damage / (bgScrollSpeed / 5);

                        playSE.PlaySE2();
                        charaAnim.SetTrigger("isCatchSucess");
                        bgManager.isGround = true;

                        Debug.Log("キャッチ成功");
                        state = STATE.CATCH_SUCSESS;
                    }
                }
                break;

            case STATE.CATCH_FAIL:
                if(Time.time > pastTime)
                {
                    GetComponent<SceneChangeScript>().SceneChange();
                }

                break;

            case STATE.CATCH_SUCSESS:
                //死亡
                if (opponentHp.GetComponent<RectTransform>().sizeDelta.x <= 0 || playerHp.GetComponent<RectTransform>().sizeDelta.x <= 0)
                {
                    SEScript playSE = GetComponent<SEScript>();
                    playSE.PlaySE();
                    charaAnim.SetTrigger("isCatchFail");
                    bgScrollSpeed = 400;
                    bgManager.isGround = false;
                    pastTime = Time.time + 2;

                    Debug.Log("HP0のため死亡");
                    state = STATE.CATCH_FAIL;
                }
                if (bgScrollSpeed <= 0)
                {
                    if (dontDestroyPara.isOpponent)
                    {
                        pastTime = Time.time + 0.5f; 
                    }
                    bgScrollSpeed = 0;
                    bgManager.isScroll = false;
                    charaAnim.SetTrigger("isIdle");
                    catchGage.gameObject.SetActive(false);
                    state = STATE.IDLE;
                }
                break;

            case STATE.STOP:
                break;

        }
    }

    //各状態時にマイフレーム行う処理
    private void StateAction()
    {
        switch (state)
        {
            case STATE.IDLE:
                break;

            case STATE.CHARGE:

                break;

            case STATE.THROW:
                break;

            case STATE.CATCH_FAIL:
                if (dontDestroyPara.isOpponent)
                {
                    bgManager.scrollSpeed = bgScrollSpeed;
                }
                else
                {
                    bgManager.scrollSpeed = -bgScrollSpeed;
                }
                break;

            case STATE.CATCH_SUCSESS:
                bgScrollSpeed -= 5;
                count++;
                if (dontDestroyPara.isOpponent)
                {
                    Vector2 hp = opponentHp.GetComponent<RectTransform>().sizeDelta;
                    bgManager.scrollSpeed = bgScrollSpeed;
                    if (hp.x >= 0)
                    {
                        opponentHp.GetComponent<RectTransform>().sizeDelta -= new Vector2(dontDestroyPara.updateDamage,0);
                    }
                    else
                    {
                        opponentHp.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 1);
                    }
                }
                else
                {
                    Vector2 hp = playerHp.GetComponent<RectTransform>().sizeDelta;
                    bgManager.scrollSpeed = -bgScrollSpeed;
                    if (hp.x >= 0)
                    {
                        playerHp.GetComponent<RectTransform>().sizeDelta -= new Vector2(dontDestroyPara.updateDamage, 0);
                    }
                    else
                    {
                        playerHp.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 1);
                    }
                }
                
                break;

            case STATE.STOP:
                break;

            case STATE.END:
                break;
        }
    }
}

