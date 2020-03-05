using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPara : MonoBehaviour
{
    public bool isLanded = false;
    public bool isOpponent = false;
    public bool isCatch = false;

    //ジャンプボールでのCPUの反応速度
    public float comJumpCatchSpeed;

    public float power;
    public float ballSpeed;


    // Start is called before the first frame update
    void Start()
    {
        comJumpCatchSpeed = 0.4f;

        int gameObjectCount = FindObjectsOfType<DontDestroyPara>().Length;

        if (gameObjectCount > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
