using UnityEngine;

//「Main」シーン中において、あみだくじの全容をみせるカメラの動きを制御します。
//「Main」シーン遷移後に最初に画面を映すカメラとなります。

public class SerectTimeCameraControling : MonoBehaviour
{   
    //自身のカメラコンポーネント
    private Camera MyCamera;

    //カメラのセットポジション
    private Vector3 SetPosition;

    //プレイモード
    private AmidakuziGenerateSetting.Enum_PlayMode enum_PlayMode;

    //あみだくじのライン数、ステージ行数、ステージプレハブの幅、長さ
    private int NumLine;
    private int NumRow;
    private float StageWidth;
    private float StageLength;

    //スクロールスピード
    [SerializeField] private float ScrollSpeed;

    //どこまでスクロールで見せるか
    private float ScrollDistance;

    //スクロールが終わったか
    private bool FinishScroll = false;
    void Start()
    {   
        //あみだくじの生成条件を取得
        enum_PlayMode = AmidakuziGenerateSetting.Instance.PlayMode;
        NumLine = AmidakuziGenerateSetting.Instance.NumLine;
        NumRow = AmidakuziGenerateSetting.Instance.NumRow;
        StageWidth = AmidakuziGenerateSetting.Instance.StageWidth;
        StageLength = AmidakuziGenerateSetting.Instance.StageLength;

        //カメラコンポーネントの取得
        MyCamera = gameObject.GetComponent<Camera>();

        //セットポジションの初期化
        SetPosition = new Vector3(-(NumLine-1) * StageWidth / 2,100,0) ;

        //視野角の調整(あみだくじのラインが全て捉えられるようにする)
        switch((NumLine - 1) / 4){

            case 3:     /*13 <= NumLine <= 16*/
                MyCamera.fieldOfView = 75;
                break;

            case 4:     /*17 <= NumLine <= 20*/
                MyCamera.fieldOfView = 90;
                break;

            default:    /*others*/
                MyCamera.fieldOfView = 60;
                break;
        }
        
        //配置
        transform.position = SetPosition;

        //プレイモードからスクロールで見せる距離を決定
        switch (enum_PlayMode)
        {
            case AmidakuziGenerateSetting.Enum_PlayMode.Nomal:
                ScrollDistance = NumRow * StageLength;
                break;

            case AmidakuziGenerateSetting.Enum_PlayMode.Endless:
                ScrollDistance = 5 * StageLength;
                break;
        }
        
    }

    
    void Update()
    {
        if (!FinishScroll)
        {
            if(transform.position.z < ScrollDistance)
            {   
                //あみだくじを上からの見せる
                transform.Translate(0,-ScrollSpeed * Time.deltaTime,0);
            }
            else
            {
                //カメラの位置をセットポジションに戻し、スクロールしないようにする
                Invoke("ResetPosition",2.0f);
                FinishScroll = true;
            }
        }
        
    }

    //カメラ位置をInvoke関数で定数秒後にセットポジションに戻すために使う
    void ResetPosition()
    {
        transform.position = SetPosition;
    }
}
