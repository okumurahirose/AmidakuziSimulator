using UnityEngine;
using Unity.Netcode;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;


public class ConnectingNetWorks : MonoBehaviour
{
    //Relayサーバーを作成し、ホストとして接続
    public async void HostWithRelay()
    {
        //Relayサーバーにｎ人が入れる部屋を作る
        var allocasion = await RelayService.Instance.CreateAllocationAsync(8);

        //作った部屋に入るためのコードを取得
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocasion.AllocationId);

        //Relayサーバーに作った部屋の接続情報をUnityTransport用に変換
        var relayServerData = AllocationUtils.ToRelayServerData(allocasion,"dtls");

        //変換した接続情報をUnityTransportにセット
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        //部屋のホストとして、ネットワークを開始
        NetworkManager.Singleton.StartHost();
    }

    //joincodeを基にクライアントとしてサーバーに参加
    public async void ClientJoinToRelay(string joinCode)
    {   
        //ホストが作った部屋の情報をコードを基に取得
        var allocasion = await RelayService.Instance.JoinAllocationAsync(joinCode);

        //参加する部屋の接続情報をUnityTransport用に変換
        var relayServerData = AllocationUtils.ToRelayServerData(allocasion,"dtls");

        //変換した接続情報をUnityTransportにセット
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        //クライアントとしてネットワークに参加
        NetworkManager.Singleton.StartClient();
    }
   
}
