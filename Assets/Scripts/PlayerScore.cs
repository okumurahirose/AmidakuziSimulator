using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;

    //ルートスコア、タイム、ゴールしたか
    public int RouteScore;
    public float Timer;
    private bool WasGoal = false;

    void Start()
    {
        Timer = 0;
    }

    void Update()
    {
        if (!WasGoal && playerMove.CanStart)
        {
            Timer += Time.deltaTime;
        }
    }

    void Goal()
    {
        WasGoal = true;
    }
    
}
