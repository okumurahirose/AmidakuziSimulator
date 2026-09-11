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

    //カウントダウンで使用する画像と効果音
    [SerializeField] private Image[] Count;
    [SerializeField] private AudioClip[] AudioClips_CountDown;
    AudioSource audioSource;

    //パブリックなスコアを変更しないように、それらをコピーして利用
    private int RouteScore;
    private string TimeScore;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        RouteScore = playerScore.RouteScore;
        TimeScore = playerScore.Timer.ToString("F1");

        StartCoroutine("Countdwon");
    }

    
    void Update()
    {
        RouteScore = playerScore.RouteScore;
        TimeScore = playerScore.Timer.ToString("F1");

        Text_RouteScore.text = "RouteScore : " + RouteScore;
        Text_TimeScore.text = "TimeScore : " + TimeScore;
    }

    IEnumerator Countdwon()
    {
        for(int i = 0;i < Count.Length; i++)
        {
            Count[i].gameObject.SetActive(true);
            Debug.Log((i+1) % Count.Length);
            audioSource.PlayOneShot(AudioClips_CountDown[(i+1) / Count.Length]);
            yield return new WaitForSeconds(1.0f);
            Count[i].gameObject.SetActive(false);
        }
    }
}
