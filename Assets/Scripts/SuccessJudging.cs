using System.Runtime.CompilerServices;
using UnityEngine;

public class SuccessJudging : MonoBehaviour
{
    public int SuccessNum;

    void OnTriggerEnter(Collider other)
    {
        if(transform.tag == "EventTrigger")
        {
            SuccessJudging player = other.gameObject.GetComponent<SuccessJudging>();
            if(SuccessNum == player.SuccessNum)
            {
                Debug.Log("OK");
            }
            else
            {
                Debug.Log("No");
            }
        }
    }
}
