using UnityEngine;

public class GoalDeciding : MonoBehaviour
{
    private PlayerMove playerMove;
    private PlayerScore playerScore;
    private Collider MyCollider;

    void Start()
    {
        MyCollider = GetComponent<Collider>();
        MyCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.position.z > transform.position.z)
        {
            Debug.Log(other.transform.position.z);
            Debug.Log(transform.position.z);
            playerMove = other.gameObject.GetComponent<PlayerMove>();
            playerMove.SendMessage("ToStan");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerScore = other.gameObject.GetComponent<PlayerScore>();
            playerScore.SendMessage("Goal");
            MyCollider.isTrigger = false;
        }
    }
}
