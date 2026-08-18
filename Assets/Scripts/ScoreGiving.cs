using UnityEngine;

public class ScoreGiving : MonoBehaviour
{   
    [SerializeField] private int PlusScore;
    private PlayerScore playerScore;

    void OnTriggerEnter(Collider other)
    {   
        Debug.Log("OK");
        playerScore = other.gameObject.GetComponent<PlayerScore>();
        playerScore.RouteScore += PlusScore;
        gameObject.SetActive(false);
    }
}
