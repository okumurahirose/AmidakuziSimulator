using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SerectLineControling : MonoBehaviour
{   
    //ステージの色付け部分を格納する配列　ColourTilesOfStartStages(スタートステージの色付けタイル)の略
    public GameObject[] CTOSS;

    //色付け部分のデフォルトマテリアル、選んでいるラインの色付け部分のマテリアル
    [SerializeField] private Material DefaultLineColour;
    [SerializeField] private Material SerectedLineColour;

    //プレイヤーオブジェクト、迫りくる壁オブジェクト
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject ClosingWall;

    //カメラオブジェクト
    [SerializeField] private GameObject SerectTimeCamera;
    private SerectTimeCameraControling serectTimeCameraControling;
    [SerializeField] private GameObject PlayTimeCamera;

    //キャンバスオブジェクト、UIConrolerオブジェクト
    [SerializeField] private GameObject SerectTimeCanvas;
    [SerializeField] private GameObject PlayTimeCanvas;
    [SerializeField] private GameObject UIControler_PlayTime;

    //プレイヤーのSuccessJudging
    [SerializeField] private SuccessJudging PlayerSuccessJudging;


    //あみだくじのライン数、プレイヤーが選んでるラインのナンバー
    private int NumLine;
    private float StageWidth;
    private int SerectedNum = 0;


    void Start()
    {   
        //あみだくじの情報を取得
        NumLine = AmidakuziGenerateSetting.Instance.NumLine;
        StageWidth = AmidakuziGenerateSetting.Instance.StageWidth;

        //コンポーネントの取得
        serectTimeCameraControling = SerectTimeCamera.GetComponent<SerectTimeCameraControling>();

        //配列の動的確保
        CTOSS = new GameObject[NumLine];

        StartCoroutine("FirstSerectedLine");
    }

    // Update is called once per frame
    void Update()
    {   
        //aキーで選択を右に
        if (Keyboard.current.aKey.wasPressedThisFrame && SerectedNum > 0)
        {   
            DontSerectLine(SerectedNum);
            SerectedNum--;
            SerectLine(SerectedNum);
        }
        //dキーで選択を左に
        if(Keyboard.current.dKey.wasPressedThisFrame && SerectedNum < NumLine-1)
        {
            DontSerectLine(SerectedNum);
            SerectedNum++;
            SerectLine(SerectedNum);
        }
        //エンターキーで確定、ゲームスタート
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {   
            //プレイヤーの成功判定番号を決定
            PlayerSuccessJudging.DecideSuccessNum(SerectedNum);

            //プレイヤー,カメラのスタート位置を決定
            Player.transform.position = new Vector3(-SerectedNum * StageWidth,3.5f,0.0f);
            PlayTimeCamera.transform.position = new Vector3(-SerectedNum * StageWidth,6.0f,-4.0f);

            //カメラ、キャンバスの切り替え、プレイヤー、迫りくる壁を顕在化
            SerectTimeCamera.SetActive(false);
            PlayTimeCamera.SetActive(true);
            PlayTimeCanvas.SetActive(true);
            UIControler_PlayTime.SetActive(true);
            ClosingWall.SetActive(true);
            Player.SetActive(true);

            //このオブジェクトの潜在化
            gameObject.SetActive(false);
        }
    }

    //一番左のラインが初めに選ばれていることにする
    IEnumerator FirstSerectedLine()
    {   
        yield return new WaitForSeconds(4.0f);
        SerectLine(0);
    }

    //選択を解除
    void DontSerectLine(int Num)
    {   
        MeshRenderer MyMeshRenderer = CTOSS[Num].GetComponent<MeshRenderer>();
        MyMeshRenderer.material = DefaultLineColour;
    }

    //ラインを選択
    void SerectLine(int SerectedNum)
    {   
        MeshRenderer MyMeshRenderer = CTOSS[SerectedNum].GetComponent<MeshRenderer>();
        MyMeshRenderer.material = SerectedLineColour;
    }
}
