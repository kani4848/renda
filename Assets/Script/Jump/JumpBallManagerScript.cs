using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JumpBallManagerScript : MonoBehaviour
{
    private float nextTime;
    private bool isJumpIdle;

    public GameObject ose;
    public Text failOrSucsess;

    public Animator jumpBallAnimator;
    public Animator playerAnimetor;
    public Animator enemyAnimetor;

    private AnimatorClipInfo[] clipInfo;
    private DontDestroyPara dontDestroyPara;

    private void Start()
    {
        dontDestroyPara = GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>();
        ose.SetActive(false);
    }

    private void Update()
    {

        clipInfo = jumpBallAnimator.GetCurrentAnimatorClipInfo(0);
     
        if (clipInfo[0].clip.name == "Jump_idle")
        {
            if (!isJumpIdle)
            {
                nextTime = Time.time + 1 + Random.value;
                isJumpIdle = true;
            }
            if (Time.time > nextTime)
            {
                ose.SetActive(true);
            }
            if (Time.time > nextTime + dontDestroyPara.comJumpCatchSpeed && isJumpIdle)
            {
                ose.SetActive(false);
                playerAnimetor.SetTrigger("isLose");
                enemyAnimetor.SetTrigger("isWin");
                failOrSucsess.text = "FAIL!";
                dontDestroyPara.isOpponent = true;
                jumpBallAnimator.SetTrigger("isCatch");
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (ose.activeSelf)
                {
                    ose.SetActive(false);
                    playerAnimetor.SetTrigger("isWin");
                    enemyAnimetor.SetTrigger("isLose");
                    failOrSucsess.text = "SUCSESS!";
                }
                else
                {
                    ose.SetActive(false);
                    playerAnimetor.SetTrigger("isLose");
                    enemyAnimetor.SetTrigger("isWin");
                    failOrSucsess.text = "FAIL!";
                    dontDestroyPara.isOpponent = true;
                }
                jumpBallAnimator.SetTrigger("isCatch");
            }
        }
    }

    public void GoToBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
