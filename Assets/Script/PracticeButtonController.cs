using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticeButtonController : MonoBehaviour
{
    public void GoToScore()
    {
        SceneManager.LoadScene("score");
    }

    public void GoToVS()
    {
        SceneManager.LoadScene("jumpBall");

    }
}
