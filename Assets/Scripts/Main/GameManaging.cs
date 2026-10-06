using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManaging : MonoBehaviour
{   
    public static GameManaging Instance;

    [SerializeField] private SceneControling sceneControling;
    [SerializeField] private UIControling_Main uIControling_Main; 

    public bool CanStart;
    public bool IsStan;
    public bool WasGoal;

    private float CountDownTime = 4.0f;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        CanStart = false;
        IsStan = false;
        WasGoal = false;
    }

    void Update()
    {   
         //ゴール後はescapeキーで元のシーンにもどる
        if ((WasGoal || IsStan) && Keyboard.current.escapeKey.isPressed)
        {   
            BGMManaging.Instance.PlayBGM(0.05f);
            sceneControling.ToGenerateSerect();
        }
    }

    public void StartCountDown()
    {
        StartCoroutine(CountDown());
    }
    
    IEnumerator CountDown()
    {
        for(int i = 0;i < CountDownTime; i++)
        {
            uIControling_Main.DisplayCountImage(i);
            SEManaging.Instance.PlayStartCountDownSE((i+1) / (int)CountDownTime);
            yield return new WaitForSeconds(1.0f);
            uIControling_Main.HideCountDownImage(i);
        }

        CanStart = true;
    }

    public void ToStan()
    {
        IsStan = true;
    }

    public void ToGoal()
    {
        WasGoal = true;
    }
}
