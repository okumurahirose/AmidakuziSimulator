using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class StageGenerating : MonoBehaviour
{
    [SerializeField] private GameObject[] Stages;
    private List<GameObject> GeneratedStages = new List<GameObject>();
    public int NumLine;
    public int NumRow;
    [SerializeField] private float CornerRate;
    private float StageWidth = 16.0f;
    private float StageLength = 20.0f;
    private int[,] Route;

    //生成するステージの種類を列挙体として宣言　（配列Stages[]は直線、曲がり角、曲がり角受けの順に設定する）
    enum KindofStage
    {
        Straight,CornerRight,CornerLeft
    }

    void Awake()
    {   
        //あみだくじのルートを記録する配列
        Route = new int[NumRow,NumLine];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        for(int Row = 0;Row < NumRow; Row++)
        {   
            //隣のステージが曲がり角であるか
            bool Corner = false;

            for(int Line = 0;Line < NumLine;Line++)
            {   
                //最初と最後のステージは直線
                if(Row == 0 || Row == NumRow-1)
                {
                    GenerateStage(Route,Line,Row,false);
                }
                //中間のステージを生成
                else
                {
                    Corner = GenerateStage(Route,Line,Row,Corner);
                }
                
            }
        }
    }

    //ステージを生成する。引数は(列番号、行番号、隣のステージが曲がり角であるか)
    bool GenerateStage(int[,] Route,int line, int row, bool Corner)
    {   
        //どの種類のステージを生成するか
        int hantei = (int)KindofStage.Straight;

        //右で生成したステージが曲がり角だったら、その受け部分のステージを選択
        if (Corner)
        {
            hantei = (int)KindofStage.CornerLeft;
        }
        //最初、最後のステージではなく、端のステージではなかったら、特定の確率で曲がり角を選択
        else if(row != 0 && row != NumRow-1 && line != NumLine-1 && Random.Range(0,99) < CornerRate)
        {
            hantei = (int)KindofStage.CornerRight;
        }

        //ステージを生成
        GameObject target = Instantiate(
                                    Stages[hantei],
                                    new Vector3(-StageWidth * line,0,StageLength * row),
                                    Quaternion.identity
                                    );
        GeneratedStages.Add(target);
        target.transform.parent = transform;

        //あみだくじのルートを確定させる
        Route[row,line] = Routing(Route,line,row,hantei);
        SuccessJudging judge = target.GetComponentInChildren<SuccessJudging>();
        judge.SuccessNum = Route[row,line];

        //生成したステージが曲がり角だったらtrueを返す
        if(hantei == (int)KindofStage.CornerRight)
        {
            return true;
        }

        return false;

    }

    //あみだくじのルートを探る(最初のステージの列番号でルートを判別)
    int Routing(int [,] Route,int line,int row,int hantei)
    {
       if(row == 0)
            return line;

        switch(hantei)
        {
            case 0:
                return Route[row-1,line];
            case 1:
                return Route[row-1,line+1];
            case 2:
                return Route[row-1,line-1];

            default :
                return -1;
        }
    }
}
