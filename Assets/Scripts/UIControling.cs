using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControling : MonoBehaviour
{   
    [SerializeField] private PlayerScore playerScore;
    [SerializeField] private TextMeshProUGUI Text_RouteScore;
    [SerializeField] private TextMeshProUGUI Text_TimeScore;

    private int RouteScore;
    private string TimeScore;

    void Start()
    {
        RouteScore = playerScore.RouteScore;
        TimeScore = playerScore.Timer.ToString("F1");
    }

    
    void Update()
    {
        RouteScore = playerScore.RouteScore;
        TimeScore = playerScore.Timer.ToString("F1");

        Text_RouteScore.text = "RouteScore : " + RouteScore;
        Text_TimeScore.text = "TimeScore : " + TimeScore;
    }
}
