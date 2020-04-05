﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class DontDestroyPara : MonoBehaviourPunCallbacks,IPunObservable
{
    public bool online = false;
    public bool isLanded = false;
    public bool isOpponent = false;
    public bool isCatch = false;

    //ジャンプボールでのCPUの反応速度
    public float comJumpCatchSpeed;

    public float power;
    public float damage;
    public float updateDamage;
    public float ballSpeed;

    public float comChargePower;
    public float comPrevent;


    // Start is called before the first frame update
    void Start()
    {
        comJumpCatchSpeed = 0.4f;
        comChargePower = 0.51f;
        comPrevent = 1f;

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

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身側が生成したオブジェクトの場合は
            // 色相値と移動中フラグのデータを送信する
            //stream.SendNext();
        }
    }
}