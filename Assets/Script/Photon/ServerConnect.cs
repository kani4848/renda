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

    private void Update()
    {
        int playerNum = PhotonNetwork.PlayerList.Length;

        if (playerNum >= 2)
        {
            GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>().online = true;
            waitText.gameObject.SetActive(false);
            SceneManager.LoadScene("JumpBall");
        }
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
        //参加可能なルームがあればそこに参加
        PhotonNetwork.JoinRandomRoom();
    }

    //ルームに入ったときに呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("ルーム参加に成功");

        base.OnJoinedRoom();
    }

    //プレイヤーが自分のルームに入った時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player player)
    {
        waitText.text = "対戦相手が見つかりました";
    }

    //ルームに入室失敗時のコールバック
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("マッチングに失敗");
        PhotonNetwork.CreateRoom(
            null,
            new RoomOptions()
            {
                MaxPlayers = 2,
                IsVisible = true,
                IsOpen = true,
            },
            TypedLobby.Default);

        base.OnJoinRandomFailed(returnCode, message);
    }
}