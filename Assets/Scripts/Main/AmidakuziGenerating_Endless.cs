using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//「Main」シーンにおいて、あみだくじを生成条件を基に作成し、各ルート情報を配列に保存します。

public class AmidakuziGenerating_Endless : MonoBehaviour
{
    [SerializeField] private SerectLineControling serectLineControling;
    [SerializeField] private UIControling_Main uIControling_Main;
    [SerializeField] private GameObject Player;

    //生成するステージプレハブ、生成したステージを保存するリスト
    [SerializeField] private GameObject[] Stages;
    private List<GameObject> GeneratedStages = new List<GameObject>();

    //あみだくじのライン数、曲がり角の生成確率
    private int NumLine;
    private float CornerRate;

    //ステージ幅、ステージ長、あみだくじの形を保存する二重配列
    private float StageWidth = 16.0f;
    private float StageLength = 20.0f;
    private int[] Route;

    //最初に作るステージの行数
    private int StartGenerateNum = 10;

    private int CurrentRow = 0;

    //生成するステージの種類を列挙体として宣言　（配列Stages[]は直線、曲がり角、曲がり角受けの順に設定する）
    enum KindofStage
    {
        Straight,CornerRight,CornerLeft
    }

    void Start()
    {   
        //あみだくじの生成条件やステージの幅、長さの情報を取得
        NumLine = AmidakuziGenerateSetting.Instance.NumLine;
        CornerRate = AmidakuziGenerateSetting.Instance.CornerRate;
        StageWidth = AmidakuziGenerateSetting.Instance.StageWidth;
        StageLength = AmidakuziGenerateSetting.Instance.StageLength;

        //あみだくじのルートを記録する配列を動的確保
        Route = new int[NumLine];

        //最初にStartGenerateNum行のあみだくじを生成
        for(;CurrentRow < StartGenerateNum; CurrentRow++)
        {   
            //隣のステージが曲がり角であるか
            bool Corner = false;

            for(int Line = 0;Line < NumLine;Line++)
            {   
                //最初のステージは直線で生成
                if(CurrentRow == 0)
                {
                    GenerateStage(Route,Line,CurrentRow,false);
                }
                //中間のステージを生成
                else
                {
                    Corner = GenerateStage(Route,Line,CurrentRow,Corner);
                }
            }
        }
    }

    void Update()
    {
        if(Player.transform.position.z > (CurrentRow-5) * StageLength)
        {   
            //隣のステージが曲がり角であるか
            bool Corner = false;

            for(int Line = 0;Line < NumLine;Line++)
            {   
                //中間のステージを生成                
                Corner = GenerateStage(Route,Line,CurrentRow,Corner);
            }

            CurrentRow++;
        }
    }

    //ステージを生成する。(引数はルート、列番号、行番号、隣のステージが曲がり角であるか)
    bool GenerateStage(int[] Route,int line,int row,bool Corner)
    {   
        //どの種類のステージを生成するか
        KindofStage hantei = KindofStage.Straight;

        //右で生成したステージが曲がり角だったら、その受け部分のステージを選択
        if (Corner)
        {
            hantei = KindofStage.CornerLeft;
        }
        //最初ステージではなく、端のステージではなかったら、特定の確率で曲がり角を選択
        else if(row != 0 && line != NumLine-1 && Random.Range(0,99) < CornerRate)
        {
            hantei = KindofStage.CornerRight;
        }

        //ステージを生成
        GameObject target = Instantiate(
                                    Stages[(int)hantei],
                                    new Vector3(-StageWidth * line,0,StageLength * row),
                                    Quaternion.identity
                                    );
        GeneratedStages.Add(target);
        target.transform.parent = transform;

        //最初のスタートステージだったらserectLineControlingに登録
        if(row == 0)
        {   
            //targetの子オブジェクトを順番に探して色付け部分を探す
            foreach(Transform child in target.transform.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("ColourTile"))
                {
                    //CTOSSはStart関数で初期化されるのでコールチンで初期化を待ってから参照
                    //CTOSSの内容を設定
                    StartCoroutine(SetCTOSSObjects(line,child.gameObject));
                    break;
                }
            }

        }

        //あみだくじのルートを確定させる
        
        Route[line] = Routing(Route,line,row,hantei);
        SuccessJudging judge = target.GetComponentInChildren<SuccessJudging>();
        judge.SuccessNum = Route[line];   

        //生成したステージが曲がり角だったらtrueを返す
        if(hantei == KindofStage.CornerRight)
        {
            return true;
        }

        return false;
    }

    //あみだくじのルートを探る(最初のステージの列番号でルートを判別)
    //今の行の判別番号のみ保存していく
    int Routing(int [] Route,int line,int row,KindofStage hantei)
    {
        if(row == 0)
            return line;

        if(hantei == KindofStage.CornerRight)
        {
            Swap(ref Route[line],ref Route[line+1]);
        }

        return Route[line];
    }

    IEnumerator SetCTOSSObjects(int line,GameObject child)
    {
        while(serectLineControling.CTOSS.Length == 0)
        {
            yield return null; 
        }

        serectLineControling.CTOSS[line] = child;
    }

    //二つの値を入れ替える
    void Swap(ref int x,ref int y)
    {
        int cp = x;
        x = y;
        y = cp;
    }
}
