using TMPro;
using UnityEngine;

//「SettimgMenu」シーンにおいて、「Main」シーンのゴールエリアに置かれたキャンバスに表示されるゴール後の一言コメントについて、プレイヤーが自由に文章を設定できるよう、InputFieldに書き込まれた文章をデータに保存します。
//また、入力パネルの入力状態に応じて、新規入力パネルを生成、削除します。

public class UIConroling_SettingMenu_PresentWord : MonoBehaviour
{   
    //ScrollViewに表示されるContentオブジェクト
    [SerializeField] private GameObject ScrollContent;
    private RectTransform ContentTransform;

    //入力パネルプレハブ、その幅、その高さ
    [SerializeField] private GameObject WordInputPalnelPrefab;

    //シーン上に最初からある入力パネル
    [SerializeField] private PresentWordInputFieldPanel[] FirstWordInputPanels;

    //シーン上にある入力パネルを記憶する配列    
    [SerializeField] private PresentWordInputFieldPanel[] WordInputPanels;

    //シーン上に存在できる入力パネルの最大数
    private int MaxPanelCount;

    //現在シーン上に存在するパネルの数（入力されてないものも含む）
    private int PanelCount;

    //パネル間の距離
    [SerializeField] private float PanelDistance;

    
    void Start()
    {   
        //情報の取得
        MaxPanelCount = GoalPresentWordData.Instance.MaxWordCount;
        PanelCount = FirstWordInputPanels.Length;

        //配列の初期化
        WordInputPanels = new PresentWordInputFieldPanel[GoalPresentWordData.Instance.MaxWordCount];

        //コンポーネントの取得
        ContentTransform = ScrollContent.GetComponent<RectTransform>();

        //初めからあるパネルをパネル配列に追加
        for(int i = 0;i < PanelCount;i++)
        {
            WordInputPanels[i] = FirstWordInputPanels[i];
        }
        

    }

    public void RegisterWords(TMP_InputField Word,RectTransform rectTransform,int Num)
    {   
        //入力された文章をデータに登録
        GoalPresentWordData.Instance.PresentWords[Num-1] = Word.text;

        //最後尾の空の入力パネルを使用しており、最大個数目のパネルじゃない場合に新しい空の入力パネルを追加
        if(Num == PanelCount && Num < MaxPanelCount)
        {   
            //Contentの長さを伸ばす
            ContentTransform.sizeDelta += new Vector2(0,PanelDistance);

            //新しい入力パネルの生成
            GameObject target = Instantiate(
                                    WordInputPalnelPrefab,
                                    Vector3.zero,
                                    Quaternion.identity
                                );

            //Contentに追加
            target.transform.parent = ScrollContent.transform;

            //配列に追加
            WordInputPanels[Num] = target.GetComponent<PresentWordInputFieldPanel>();

            //サイズ調整と配置
            RectTransform targetTransform = target.GetComponent<RectTransform>();
            targetTransform.localScale = Vector3.one;
            targetTransform.anchoredPosition = new Vector2(0,rectTransform.anchoredPosition.y - PanelDistance);

            //生成したtargetのPresentWordInputFieldPanelのUIControlingがこのスクリプトになるようにする
            WordInputPanels[Num].UIConroling = this;

            //生成したパネルの番号を決定
            WordInputPanels[Num].NumberText.text = (Num+1).ToString();

            //パネル増やした分と新たに文章を確定した分、一つずつ増やす
            PanelCount++;
            GoalPresentWordData.Instance.WordCount++;
        }
        else if(PanelCount == MaxPanelCount)
        {   
            //パネルが最大個数存在し、最大個数目が確定したときパネルは増えないため、登録文章数だけ増やす
            GoalPresentWordData.Instance.WordCount++;
        }
    }

    public void DeletePanel(GameObject Panel,int Num)
    {
        if(PanelCount > 1 && PanelCount != Num)
        {
            Destroy(Panel);
            int i;

            for(i = Num;i < PanelCount; i++)
            {   
                //パネルオブジェクト配列の一要素を消した分一つずつ詰める
                WordInputPanels[i-1] = WordInputPanels[i];

                //データの方も一つずつ詰める
                GoalPresentWordData.Instance.PresentWords[i-1] = GoalPresentWordData.Instance.PresentWords[i]; 

                //パネルの位置を一つずつ詰めて、番号も一つずつ若くする
                RectTransform PanelTransform = WordInputPanels[i-1].gameObject.GetComponent<RectTransform>();
                PanelTransform.anchoredPosition += new Vector2(0,PanelDistance);
                WordInputPanels[i-1].NumberText.text = i.ToString();
            }

            //詰めた分を一つ分を削除
            WordInputPanels[i-1] = null;
            GoalPresentWordData.Instance.PresentWords[i-1] = null;

            //Contentの長さを縮ませる
            ContentTransform.sizeDelta -= new Vector2(0,PanelDistance);

            //パネルを減らした分と、文章を削除した分で一つずつ減らす
            PanelCount--;
            GoalPresentWordData.Instance.WordCount--;
        }
        else if(PanelCount == 1)
        {   
            //パネルが一個しかないときにそれが空なら、パネルは減らないため登録文章数だけを減らす
            GoalPresentWordData.Instance.WordCount--;
        }
    }
}
