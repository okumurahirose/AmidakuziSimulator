using UnityEngine;
using UnityEngine.SceneManagement;

//すべてのプレイヤーオブジェクトを統一して管理します。
//プレイヤーの登録、生成する各プレイヤーの情報管理、全員がゴールしたか、など全体での運用に関して使用します。
//シングルトンとして運用し、シーンを跨いで情報を保持します。

public class AllPlayersData : MonoBehaviour
{
    public static AllPlayersData Instance;

    [SerializeField] private SceneControling sceneControling;

    //オフラインプレイ(1人プレイ)時のプレイヤーオブジェジェクト
    [SerializeField] private GameObject OfflinePlayer;

    //プレイヤーオブジェクトを保存する配列
    [SerializeField] private GameObject[] Players;

    //同時対戦できる最大人数
    private int MaxPlayerNum = 20;

    //同時対戦している人数
    private int PlayerNum;

    //ゴールした人数
    private int WasGoalPlayersNum = 0;

    //ゴールしたプレイヤーオブジェクトを保存する配列
    private GameObject[] WasGoalPlayers;

    void Start()
    {
        //オフラインのシーンであるならば、プレイヤーオブジェジェクトを指定して保存
        if(SceneManager.GetActiveScene().name == "Main")
        {   
            //配列の動的配置
            PlayerNum = 1;
            Players = new GameObject[PlayerNum];
            WasGoalPlayers = new GameObject[PlayerNum];

            //プレイヤーオブジェクトの保存
            Players[0] = OfflinePlayer;
        }
    }

    //ゴールしたプレイヤーオブジェクトを保存する
    public void RegisterGoalPlayer(GameObject Player)
    {   
        //保存してゴールした人数を増やす
        WasGoalPlayers[WasGoalPlayersNum] = Player;
        WasGoalPlayersNum++;

        //すべてのプレイヤーがゴールしたら前のシーンに戻る
        if(WasGoalPlayersNum == PlayerNum)
        {
            Invoke("BackToScene_Offline",5.0f);
        }
    }

    //「GenerateSerect」シーンに戻る
    void BackToScene_Offline()
    {
        sceneControling.ToGenerateSerect();
    }
}
