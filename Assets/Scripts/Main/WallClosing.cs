using UnityEngine;

//「Main」シーンにおいて、迫りくる壁の動きを制御します。

public class WallClosing : MonoBehaviour
{   
    [SerializeField] private Material material;

    //追いかける速さ、初期のｚ位置
    [SerializeField] private float ClosingSpeed; //[m/s]
    [SerializeField] private float FirstPosition_Z; //[m]

    //あみだくじのライン数、ステージ行数
    private int NumLine;
    private int NumRow;

    //プレイモード
    private AmidakuziGenerateSetting.Enum_PlayMode enum_PlayMode;


    //壁の高さ、壁の幅、ステージ一個の幅、Scale(1,1)に対するマテリアルのタイリング比
    private float WallHeight = 8.0f; //[m]
    private float WallWidth; //[m]
    private float StageWidth; //[m]
    private float StageLength; //[m]
    private float Tilling = 5; //[]g
    
    
    void Start()
    {   
        //あみだくじの生成条件やステージプレハブの幅、長さの情報を取得
        NumLine = AmidakuziGenerateSetting.Instance.NumLine;
        NumRow = AmidakuziGenerateSetting.Instance.NumRow;
        StageWidth = AmidakuziGenerateSetting.Instance.StageWidth;
        StageLength = AmidakuziGenerateSetting.Instance.StageLength;
        enum_PlayMode  = AmidakuziGenerateSetting.Instance.PlayMode;

        //壁のサイズとポジションをあみだくじの大きさに合わせて調整
        WallWidth = NumLine * StageWidth;
        transform.localScale = new Vector3(WallWidth,WallHeight,1);
        transform.position = new Vector3(-(WallWidth-StageWidth)/2,WallHeight/2,FirstPosition_Z);
        material.mainTextureScale = new Vector2(WallWidth * Tilling,WallHeight * Tilling);
    }

    void Update()
    {
        //ノーマルモードの場合ゴールとの境目で止まる
        if (enum_PlayMode == AmidakuziGenerateSetting.Enum_PlayMode.Nomal && transform.position.z > NumRow * StageLength + StageLength /2 - 1.0f)
        {
            return;
        }
        
        //壁を前進させる
        transform.Translate(0,0,ClosingSpeed * Time.deltaTime);   
    }
}
