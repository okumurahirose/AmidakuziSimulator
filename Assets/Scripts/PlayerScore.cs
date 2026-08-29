using UnityEngine;

//「Main」シーンにおいて、プレイヤーが保持するスコアに関する情報を計算、保存します。
//各プレイヤーオブジェクトに付与され、個別管理となります。

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;

    //ルートスコア、タイム、ゴールしたか
    public int RouteScore;
    public float Timer;
    private bool WasGoal = false;

    void Start()
    {
        Timer = 0;
    }

    void Update()
    {
        if (!WasGoal && playerMove.CanStart)
        {
            Timer += Time.deltaTime;
        }
    }

    void Goal_Score()
    {
        WasGoal = true;
    }
    
}
