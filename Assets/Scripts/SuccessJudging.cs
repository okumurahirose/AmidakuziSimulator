using System.Runtime.CompilerServices;
using UnityEngine;

public class SuccessJudging : MonoBehaviour
{
    //このルートを通れる識別番号
    public int SuccessNum; //[m]
    Animator animator;

    void Start()
    {   
        //このオブジェクトのタグがSuccessJudgerだった場合にアニメーターを取得
        if(transform.tag == "SuccessJudger")
        {
            animator = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter(Collider other)
    {   
        //SuccessJudger側で成功判定を行う
        if(transform.tag == "SuccessJudger")
        {   
            //プレイヤーが持つ番号と識別番号が合っていたら成功
            SuccessJudging player = other.gameObject.GetComponent<SuccessJudging>();
            if(SuccessNum == player.SuccessNum)
            {
                Debug.Log("OK");
            }
            else
            {   
                //失敗したらプレイヤーに失敗判定を送り、スタン状態にする
                other.gameObject.SendMessage("ToStan");
                if(gameObject.name == "イベントトリガー")
                {
                    animator.SetTrigger("RouteFalt");
                }
                
            }
        }
    }
}
