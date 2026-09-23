using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using System.Linq;
using System;



public class LobbyCreating : MonoBehaviour
{   
    //UIが追加されるスクロールのコンテントと追加するUI
    [SerializeField] private GameObject ScrollViewContent;
    [SerializeField] private GameObject AllocationPanel;

    //UIパネルの初期位置、パネル間の距離、パネルの枚数
    private float FirstAllocationPanelPoint = -100.0f;
    private float AllocationPanelDistance = 160.0f;
    private float AllocationPanelNum;

    async void Start()
    {
        try
        {
            //Unity Gaming Service を初期化（Relay、Lobby、Authenticationを使うためのサービス）
            await UnityServices.Instance.InitializeAsync();
            
            //認証してないプレイヤーを認証
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                //シーンに到達したプレーヤーを匿名で認証する(入ってきた段階では全員がクライアント)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            

            Debug.Log("プレイヤーの認証に成功 ID : " + AuthenticationService.Instance.PlayerId);
        }
        catch
        {
            Debug.Log("プレイヤーの認証に失敗");
        }
    }

    //ロビーを構築する
    public async void CreateLobby()
    {
        string LobbyName = "TestLobby";
        int MaxPlayerNum = 1;
        Lobby hostLobby;

        try
        {
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions()
            {
                IsPrivate = false,
                Player = new Player(AuthenticationService.Instance.PlayerId){
                    Data = new Dictionary<string, PlayerDataObject>{
                        //ロビーを作った人はホストになることをデータに保存
                        {"Position",new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member,"Host")}
                    }
                }
            };

            hostLobby = await LobbyService.Instance.CreateLobbyAsync(LobbyName,MaxPlayerNum,createLobbyOptions);

            AddAllocationPanel(hostLobby);

            Debug.Log("ロビーの作成に成功 ID : " + hostLobby.LobbyCode);
        }
        catch(ArithmeticException e)
        {
            Debug.Log("ロビーの作成に失敗");
            Debug.LogError(e);
        }
    }

    private void AddAllocationPanel(Lobby hostLobby)
    {
        AllocationPanelNum++;

        //パネルの生成
        GameObject Target = Instantiate(
                                AllocationPanel,
                                Vector3.zero,
                                Quaternion.identity
                                );
        
        //パネルの親子関係の設定
        Target.transform.SetParent(ScrollViewContent.transform,false);
        
        //サイズ調整と配置
        RectTransform targetTransform = Target.GetComponent<RectTransform>();
        targetTransform.localScale = Vector3.one;
        targetTransform.anchoredPosition = new Vector2(0,-AllocationPanelDistance * (AllocationPanelNum-1) + FirstAllocationPanelPoint);

        //パネルに付いているロビーを監視するコンポーネントに、監視対象となるロビーを渡す
        LobbyManaging lobbyWatching = Target.GetComponent<LobbyManaging>();
        lobbyWatching.MyLobby = hostLobby;
    }
}
