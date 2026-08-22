using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControling : MonoBehaviour
{   
    [SerializeField] private PlayerScore playerScore;
    [SerializeField] private TextMeshProUGUI Text_RouteScore;
    [SerializeField] private TextMeshProUGUI Text_TimeScore;
    [SerializeField] private Image[] Count;

    private int RouteScore;
    private string TimeScore;

    void Start()
    {
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
            yield return new WaitForSeconds(1.0f);
            Count[i].gameObject.SetActive(false);
        }
    }
}
