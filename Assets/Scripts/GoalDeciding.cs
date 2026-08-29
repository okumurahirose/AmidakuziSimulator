using System.Collections;
using UnityEngine;

//「Main」シーンにおいて、プレイヤーがゴールエリアに達したことを検知し、プレイヤーの動きやスコアを管理するオブジェクトに信号を送信します。
//また、プレイヤーがゴール後に逆走を計れないよう、ゴールエリアとステージの境目に置かれた自身の当たり判定を操作します。

public class GoalDeciding : MonoBehaviour
{
    public AllPlayersData allPlayersData;
    private PlayerMove playerMove;
    private PlayerScore playerScore;
    private Collider MyCollider;

    void Start()
    {
        MyCollider = GetComponent<Collider>();
        MyCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.position.z > transform.position.z)
        {
            playerMove = other.gameObject.GetComponent<PlayerMove>();
            playerMove.SendMessage("ToStan");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerScore = other.gameObject.GetComponent<PlayerScore>();
            playerMove = other.gameObject.GetComponent<PlayerMove>();
            playerScore.SendMessage("Goal_Score");
            StartCoroutine("GoalMessageToMove");
            allPlayersData.RegisterGoalPlayer(other.gameObject);
            MyCollider.isTrigger = false;
        }
    }

    IEnumerator GoalMessageToMove()
    {
        yield return new WaitForSeconds(1.0f);
        playerMove.SendMessage("Goal_Move");
    }
}
