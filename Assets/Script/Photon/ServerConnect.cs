using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ServerConnect : MonoBehaviourPunCallbacks
{
    //プレイヤーの識別
    //シーン遷移
    public Text waitText;

    private void Start()
    {
        waitText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    public void MasterServerConnect()
    {
        //PhotonServerSettingsに設定した内容を使ってマスターサーバーへ接続する
        Debug.Log("接続を試みる ") ;
        waitText.gameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    //マスターサーバーへの接続が成功した後に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーに接続完了");
        //マッチングルームを作成するメソッド
        //第一引数はルーム名。nullならランダムな重複しない名称をつける。
        //ルームを作ったプレイヤーは自動的にそのルームに参加する。
        PhotonNetwork.CreateRoom(
            null,
            new RoomOptions() { 
                MaxPlayers = 2,
            }
            );

        //OnJoinedRoom();
        //base.OnConnectedToMaster();
    }

    //ルームに入ったときに呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        int playerNum =  GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>().playerNum;
        playerNum++;
        Debug.Log("ルーム参加に成功、プレイヤーの人数は" + playerNum +"人です。");

        if (playerNum >= 2)
        {
            GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>().online = true;
            waitText.gameObject.SetActive(false);
            SceneManager.LoadScene("JumpBall");
        }
        base.OnJoinedRoom();
    }

    //ルームに入室失敗時のコールバック
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("マッチングに失敗");
        base.OnJoinRandomFailed(returnCode, message);
    }
}