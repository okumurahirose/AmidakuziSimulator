using System;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class SEManaging : MonoBehaviour
{
    public static SEManaging Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] ButtonSE;
    [SerializeField] private AudioClip[] StartCountDownSE;
    [SerializeField] private AudioClip RouteScoreSE;
    [SerializeField] private AudioClip StanSE;
    [SerializeField] private AudioClip StanPanelSE;
    [SerializeField] private AudioClip[] GoalSE;

    void Awake()
    {
        if(Instance == null)
        {
            Instance  = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonSE(int index)
    {
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(ButtonSE[index]);
    }

    public void PlayStartCountDownSE(int index)
    {   
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(StartCountDownSE[index]);
    }

    public void PlayRouteScoreSE()
    {
        audioSource.volume = 1.0f;
        audioSource.PlayOneShot(RouteScoreSE);
    }

    public void PlayStanSE()
    {
        audioSource.volume = 1.0f;
        audioSource.PlayOneShot(StanSE);
    }

    public void PlayStanPanelSE()
    {
        audioSource.volume = 0.6f;
        audioSource.PlayOneShot(StanPanelSE);
    }

    public void PlayGoalSE()
    {   
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(GoalSE[0]);
        audioSource.PlayOneShot(GoalSE[1]);
    }
}
