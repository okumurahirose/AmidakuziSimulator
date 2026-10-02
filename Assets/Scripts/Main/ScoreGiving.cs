using UnityEngine;

//「Main」シーン中において、プレイヤーのルートスコアを上げるスコアギフターオブジェクトの挙動を管理します。
//各スコアギフターオブジェクトに付与され、個別管理となります。

public class ScoreGiving : MonoBehaviour
{   
    //一回のスコア付与であげる得点
    [SerializeField] private int PlusScore;
    
    //プレイヤーのスコアコンポーネント
    private PlayerScore playerScore;

    void OnTriggerEnter(Collider other)
    {   
        playerScore = other.gameObject.GetComponent<PlayerScore>();
        playerScore.RouteScore += PlusScore;
        SEManaging.Instance.PlayRouteScoreSE();
        gameObject.SetActive(false);
    }
}