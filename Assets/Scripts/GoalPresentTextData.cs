using UnityEngine;

public class GoalPresentTextData : MonoBehaviour
{
    public static GoalPresentTextData Instance;

    //おみくじ箱の上に表示する一言を保存する配列
    public string[] PresentWords = new string[20];

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
