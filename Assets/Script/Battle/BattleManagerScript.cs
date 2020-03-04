using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleManagerScript : MonoBehaviour
{
    //スピード変換前のパワー
    public int power = 0;
    //パワーをもとに算出するボールスピード
    public float ballSpeed = 0;

    //制限時間用変数
    private float pastTime;
    private float timeLimit = 1;

    //参照するGameObject群
    public Animator charaAnim;
    public RendaButtonConroller rendaButton;
    public BallPowerGage ballPowerGage;
    public BgManager bgManager;
    private DontDestroyPara dontDestroyPara;

    public enum STATE
    {
        IDLE = -1,
        CHARGE,
        THROW,
        CATCH,
        CATCH_LOOP,
        STOP,

        END
    }
    public STATE state;

    private void Start()
    {
        dontDestroyPara = GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>();
        
        if (dontDestroyPara.isLanded)
        {
            charaAnim.SetTrigger("isLanded");
            state = STATE.CATCH;
        }
        else
        {
            state = STATE.IDLE;
        }

        if (dontDestroyPara.isOpponent)
        {
            charaAnim.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        bgManager = GetComponent<BgManager>();
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
                rendaButton.transform.position = new Vector3(0, -630, 0);
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
                    ballPowerGage.isCharge = false;
                    state = STATE.THROW;
                    charaAnim.SetTrigger("isThrow");
                    rendaButton.transform.position = new Vector3(0,2000,0); 
                    pastTime = Time.time + timeLimit;
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
                
                if(Time.time > timeLimit)
                {
                    bgManager.isScroll = true;
                    bgManager.isGround = true;

                    charaAnim.SetTrigger("isCatchSucess");
                    if (dontDestroyPara.isOpponent)
                    {
                        bgManager.isReverse = true;
                    }
                    else
                    {
                        bgManager.isReverse = false;
                    }
                    state = STATE.CATCH_LOOP;
                }
                break;

            case STATE.CATCH_LOOP:
                if (Time.time > timeLimit)
                {
                    bgManager.isScroll = false;
                    charaAnim.SetTrigger("isIdle");
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
                AnimatorClipInfo[] clipInfo = charaAnim.GetCurrentAnimatorClipInfo(0);

                if (clipInfo[0].clip.name == "jump_idle")
                {
                    //時間内にボタンを押さなければボールとられる処理
                    //ボタンを押してボールを取る処理
                }
                if (clipInfo[0].clip.name == "jump_catch")
                {
                }
                break;

            case STATE.CATCH:
                break;

            case STATE.STOP:
                break;

            case STATE.END:
                break;
        }
    }
}

