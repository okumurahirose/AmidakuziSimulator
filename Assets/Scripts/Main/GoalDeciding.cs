using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//「Main」シーンにおいて、プレイヤーがゴールエリアに達したことを検知し、プレイヤーの動きやスコアを管理するオブジェクトに信号を送信します。
//また、プレイヤーがゴール後に逆走を計れないよう、ゴールエリアとステージの境目に置かれた自身の当たり判定を操作します。

public class GoalDeciding : MonoBehaviour
{   
    public UIControling_Main uIControling_Main;
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
            GameManaging.Instance.ToStan();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManaging.Instance.ToGoal();
            MyCollider.isTrigger = false;
            SEManaging.Instance.PlayGoalSE();
        }
    }
}
