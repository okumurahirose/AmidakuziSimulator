using UnityEngine;

//「Main」シーン中で生成されるあみだくじの本体の生成条件や、構成要素である1ステージの幅、長さなどの情報を保存します。
//シングルトンとして運用し、シーンを跨いで情報を保持します。

public class AmidakuziGenerateSetting : MonoBehaviour
{
    public static AmidakuziGenerateSetting Instance;

    //あみだくじのライン数、ステージ行数、曲がり角の生成確率
    public int NumLine;
    public int NumRow;
    public float CornerRate; 

    //ステージ幅、ステージ長  (不変)
    public float StageWidth = 16.0f;
    public float StageLength = 20.0f;

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

    public void PassingSetting_NumLine(float value)
    {
        NumLine = (int)value;
    }

    public void PassingSetting_NumRow(float value)
    {
        NumRow = (int)value;
    }

    public void PassingSetting_CornerRate(float value)
    {
        CornerRate = value;
    }
}
