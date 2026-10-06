using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//「Main」シーンにおいて、画面に表示されるUIを管理します。

public class UIControling_Main : MonoBehaviour
{   
    //プレイヤースコア
    [SerializeField] private PlayerScore playerScore;

    //UIに表示する各スコアのテキスト
    [SerializeField] private TextMeshProUGUI Text_RouteScore;
    [SerializeField] private TextMeshProUGUI Text_TimeScore;

    //カウントダウンで使用する画像
    [SerializeField] private Image[] Count;

    //ゴール後に表示する案内テキスト
    [SerializeField] private TextMeshProUGUI GuideTexts;

    //スタン状態になった時に表示するパネル
    [SerializeField] private GameObject StanPanel;

    private bool StopUpdate = false;

    //パブリックなスコアを変更しないように、それらをコピーして利用
    private int RouteScore;
    private string TimeScore;

    void Start()
    {
        RouteScore = playerScore.RouteScore;
        TimeScore = playerScore.Timer.ToString("F1");
    }

    
    void Update()
    {   
        if(StopUpdate) return;

        RouteScore = playerScore.RouteScore;
        TimeScore = playerScore.Timer.ToString("F1");

        Text_RouteScore.text = "RouteScore : " + RouteScore;
        Text_TimeScore.text = "TimeScore : " + TimeScore;

        if (GameManaging.Instance.WasGoal || GameManaging.Instance.IsStan)
        {
            GuideTexts.gameObject.SetActive(true);
            Invoke("DisplayStanPanel",1.0f);
            StopUpdate = true;
        }
    }

    public void DisplayCountImage(int index)
    {
        Count[index].gameObject.SetActive(true);
    }

    public void HideCountDownImage(int index)
    {
        Count[index].gameObject.SetActive(false);
    }

    private void DisplayStanPanel()
    {   
        SEManaging.Instance.PlayStanPanelSE();
        StanPanel.SetActive(true);
    }
}
