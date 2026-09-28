using TMPro;
using UnityEngine;

//「Main」シーンにおいて、プレイヤーが保持するスコアに関する情報を計算、保存します。

public class PlayerScore : MonoBehaviour
{
    private PlayerMove playerMove;

    //ルートスコア、タイム、ゴールしたか
    public int RouteScore;
    public float Timer;
    private bool WasGoal = false;
    private bool IsStan = false;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        Timer = 0;
    }

    void Update()
    {
        if (!WasGoal && !IsStan && playerMove.CanStart)
        {
            Timer += Time.deltaTime;
        }
    }

    void ToStan()
    {
        IsStan = true;
    }

    void Goal_Score()
    {
        WasGoal = true;
    }
    
}
