using UnityEngine;

public class Scoring : MonoBehaviour
{
    private float Timer;

    void Start()
    {
        Timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
    }
}
