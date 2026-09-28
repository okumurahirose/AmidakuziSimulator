using UnityEngine;

//「Main」シーン中において、ゴールエリアに置かれたキャンバスに表示されるゴール後の一言コメントについて、その文章本体と文章数などを保存します。
//シングルトンとして運用し、シーンを跨いで情報を保持します。

public class GoalPresentWordData : MonoBehaviour
{
    public static GoalPresentWordData Instance;

    //おみくじ箱の上に表示する一言を保存する配列
    public string[] PresentWords = new string[20];

    //登録できる最大一言数
    public int MaxWordCount = 20;

    //登録された一言の数
    public int WordCount;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
