using UnityEngine;

//「Main」シーン中において、プレイヤーのルートスコアを上げるスコアギフターオブジェクトの挙動を管理します。
//各スコアギフターオブジェクトに付与され、個別管理となります。

public class ScoreGiving : MonoBehaviour
{   
    [SerializeField] private int PlusScore;
    private PlayerScore playerScore;

    void OnTriggerEnter(Collider other)
    {   
        playerScore = other.gameObject.GetComponent<PlayerScore>();
        playerScore.RouteScore += PlusScore;
        gameObject.SetActive(false);
    }
}
