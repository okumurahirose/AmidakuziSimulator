using Unity.VisualScripting;
using UnityEngine;

//「Main」シーンにおいて、プレイヤーが正しい道を通っているかを判断します。
//プレイヤー側とルート側がそれぞれ持っている識別番号によって可否を下します。
//トリガー設定したコライダーを持つ各オブジェクトに付与され、個別管理となります。

public class SuccessJudging : MonoBehaviour
{
    //このルートを通れる識別番号
    public int SuccessNum; //[m]
    Animator animator;

    void Start()
    {   
        //このオブジェクトのタグがSuccessJudgerだった場合にアニメーターを取得
        if(tag == "SuccessJudger")
        {
            animator = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter(Collider other)
    {   
        //SuccessJudger、ClosingWall側で成功判定を行う
        if(tag == "SuccessJudger" || tag == "ClosingWall")
        {
            //プレイヤーが持つ番号と識別番号が合っていたら成功
            SuccessJudging player = other.gameObject.GetComponent<SuccessJudging>();
           
            if(SuccessNum != player.SuccessNum)
            {   
                
                //失敗したらGameManagerに失敗判定を送り、スタン状態にする
                GameManaging.Instance.ToStan();
                
                if(tag == "SuccessJudger")
                {
                    animator.SetTrigger("RouteFalt");
                    SEManaging.Instance.PlayStanSE();
                }
            } 
        }
    }

    public void DecideSuccessNum(int Num)
    {
        SuccessNum = Num;
    }
}
