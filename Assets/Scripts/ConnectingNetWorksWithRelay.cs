using UnityEngine;
using Unity.Netcode;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Netcode.Transports.UTP;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEditor.Profiling.Memory.Experimental;
using System.Threading.Tasks;


public class ConnectingNetWorksWithRelay : MonoBehaviour
{
    public static ConnectingNetWorksWithRelay Instance;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Relayサーバーを作成し、ホストとして接続
    public async Task<string> HostWithRelay(int PlayerNum)
    {
        //Relayサーバーにｎ人が入れる部屋を作る
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(PlayerNum);

        //作った部屋に入るためのコードを取得
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        //Relayサーバーに作った部屋の接続情報をUnityTransport用に変換
        RelayServerData relayServerData =  new RelayServerData(allocation,"dtls");

        //変換した接続情報をUnityTransportにセット
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        //部屋のホストとして、ネットワークを開始
        NetworkManager.Singleton.StartHost();

        return joinCode;
    }

    //joincodeを基にクライアントとしてサーバーに参加
    public async Task ClientJoinToRelay(string joinCode)
    {   
        //ホストが作った部屋の情報をコードを基に取得
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        //参加する部屋の接続情報をUnityTransport用に変換
        RelayServerData relayServerData = new RelayServerData(allocation,"dtls");

        //変換した接続情報をUnityTransportにセット
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        //クライアントとしてネットワークに参加
        NetworkManager.Singleton.StartClient();
    }
   
}
