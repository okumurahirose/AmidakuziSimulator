using UnityEngine;

public class BGMManaging : MonoBehaviour
{
   public static BGMManaging Instance;
   private AudioSource audioSource;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
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

    public void PlayBGM(float volume)
    {
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void EndBGM()
    {
        audioSource.Stop();
    }
}
