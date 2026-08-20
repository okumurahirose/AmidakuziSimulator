using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int RouteScore;
    public float Timer;
    private bool WasGoal = false;

    void Start()
    {
        Timer = 0;
    }

    void Update()
    {
        if (!WasGoal)
        {
            Timer += Time.deltaTime;
        }
    }

    void Goal()
    {
        WasGoal = true;
    }
    
}
