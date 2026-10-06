using TMPro;
using UnityEngine;

//「Main」シーンにおいて、プレイヤーが保持するスコアに関する情報を計算、保存します。

public class PlayerScore : MonoBehaviour
{
    private PlayerMove playerMove;

    //ルートスコア、タイム
    public int RouteScore;
    public float Timer;

    //エンドレスモードにおいて、スピードアップのための基準値、基準値を上げるための定数
    private float CheckRouteScoreBase = 50.0f;
    private float MultipeBase = 1.5f;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        Timer = 0;
    }

    void Update()
    {
        if (!GameManaging.Instance.WasGoal && !GameManaging.Instance.IsStan && GameManaging.Instance.CanStart)
        {
            Timer += Time.deltaTime;
        }

        //エンドレスモードでありスコアが一定値を超えた時
        if(AmidakuziGenerateSetting.Instance.PlayMode == AmidakuziGenerateSetting.Enum_PlayMode.Endless && RouteScore > CheckRouteScoreBase)
        {
            playerMove.SendMessage("MaxSpeedUP");
            CheckRouteScoreBase *= MultipeBase;
        }
    }
}
