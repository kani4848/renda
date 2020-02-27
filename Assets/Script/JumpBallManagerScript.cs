using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBallManagerScript : MonoBehaviour
{
    public Animator jumpBallAnimator;
    public Animator playerAnimetor;
    public Animator enemyAnimetor;

    private AnimatorClipInfo[] clipInfo;

    private void Update()
    {

        clipInfo = jumpBallAnimator.GetCurrentAnimatorClipInfo(0);
     
        if (clipInfo[0].clip.name == "jump_idle")
        {
            //時間内にボタンを押さなければボールとられる処理
            //ボタンを押してボールを取る処理
        }
        if(clipInfo[0].clip.name == "jump_catch")
        {
            playerAnimetor.SetTrigger("isWin");
            enemyAnimetor.SetTrigger("isLose");
        }
    }
}
