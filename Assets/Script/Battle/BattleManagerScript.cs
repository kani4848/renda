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
    private float timeLimit = 2;

    //参照するGameObject群
    public Animator charaAnim;
    public RendaButtonConroller rendaButton;
    public BallPowerGage ballPowerGage;
    private DontDestroyPara dontDestroyPara;

    public enum STATE
    {
        IDLE = -1,
        CHARGE,
        THROW,
        CATCH,
        OPPONENT_CATCH,
        OPPONENT_CHARGE,
        OPPONENT_THROW,
        STOP,

        END
    }
    public STATE state;

    private void Start()
    {
        dontDestroyPara = GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>();
        state = STATE.IDLE;

        if (dontDestroyPara.isLanded)
        {
            charaAnim.SetTrigger("isLanded");
        }
        if (dontDestroyPara.isOpponent)
        {
            charaAnim.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (dontDestroyPara.isCatch)
        {
            charaAnim.SetTrigger("isCatch");
            state = STATE.CATCH;
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
                dontDestroyPara.isCatch = true;
                state = STATE.END;
                break;

            case STATE.CATCH:
                charaAnim.SetTrigger("isCatchSucess");
                state = STATE.IDLE;
                break;

            case STATE.OPPONENT_CATCH:
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

            case STATE.OPPONENT_CATCH:
                break;

            case STATE.STOP:
                break;

            case STATE.END:
                break;
        }
    }
}
