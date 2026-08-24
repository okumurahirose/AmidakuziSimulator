using Unity.VisualScripting;
using UnityEngine;

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
}
