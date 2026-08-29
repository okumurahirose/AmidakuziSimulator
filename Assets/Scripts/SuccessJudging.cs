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

    public void DecideSuccessNum(int Num)
    {
        SuccessNum = Num;
    }
}
