using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyPara : MonoBehaviour
{
    public bool isLanded = false;
    public bool isOpponent = false;
    public bool isCatch = false;

    //ジャンプボールでのCPUの反応速度
    public float comJumpCatchSpeed;

    public float power;
    public float ballSpeed;
    public float comBallSpeed;


    // Start is called before the first frame update
    void Start()
    {
        comJumpCatchSpeed = 0.4f;
        comBallSpeed = 400;

        int gameObjectCount = FindObjectsOfType<DontDestroyPara>().Length;

        if (gameObjectCount > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "KO")
        {
            Destroy(gameObject);
        }
    }
}
