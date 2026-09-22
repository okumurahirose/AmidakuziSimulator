using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Newtonsoft.Json;
using Unity.Android.Gradle.Manifest;

public class LobbyWatching : MonoBehaviour
{
    //監視対象となるロビー、ロビーに入れる人数
    public Lobby MyLobby;
    public int MaxPlayerNum = 1;

    //監視するインターバルとタイマー
    private float PollInterval = 5.0f;
    private float Timer = 0.0f;


    async Task Update()
    {
        if(MyLobby == null)
        {
            Debug.Log("監視対象となるロビーが存在しません");
            Destroy(gameObject);
        }

        //インターバル事にLobbyを監視
        Timer += Time.deltaTime;
        if(Timer >= PollInterval)
        {
            Timer = 0.0f;
            Debug.Log("ロビーを監視");
            PollLobby();
        }
    }

    //ロビーを監視するとともに、ロビーの状態を更新
    async void PollLobby()
    {
        //ロビーの状態を更新
        MyLobby = await LobbyService.Instance.GetLobbyAsync(MyLobby.Id);
        
        //ロビーにいるプレイヤー
        int PlayerNum = MyLobby.Players.Count;
        Debug.Log("ロビー人数 : " + MyLobby.Players.Count);

        //ロビーが満たされたらRelayを開始、ゲームスタート
        if(PlayerNum >= MaxPlayerNum)
        {
            Debug.Log("ロビーが満たされました");
        }
    }

    //ボタンを押したプレイヤーをロビーに参加させる
    public async Task RegistNewPlayer()
    {
        //既にロビーに参加している人をはじく
        foreach(var KnownPlayer in MyLobby.Players)
        {
            if(KnownPlayer.Id == AuthenticationService.Instance.PlayerId)
            {
                Debug.Log("既にロビーに参加しています");
                return;
            }
        }

        var PlayerId = AuthenticationService.Instance.PlayerId;

        //ボタンを押した人をロビーに参加させる
        await Lobbies.Instance.JoinLobbyByIdAsync(MyLobby.Id);

        //ボタンを押した人の情報を保存
        await Lobbies.Instance.UpdatePlayerAsync(
            MyLobby.Id,
            PlayerId,
            new UpdatePlayerOptions{
                Data = new Dictionary<string, PlayerDataObject>
                {   
                    //新しく参加したプレイヤーはクライアントになることをデータに保存
                    {$"{PlayerId}",new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member,"Client")}
                }
            }
        );

        //誰かが参加したらロビーの状態を更新
        PollLobby();

        Debug.Log($"{PlayerId}がロビーに参加");
        Debug.Log(MyLobby.Data[$"Ready_{PlayerId}"].Value);
    }
}
