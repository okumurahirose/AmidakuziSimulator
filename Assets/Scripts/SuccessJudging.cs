using System.Runtime.CompilerServices;
using UnityEngine;

public class SuccessJudging : MonoBehaviour
{
    public int SuccessNum;
    Animator StageAnimator;

    void Start()
    {
        if(transform.tag == "EventTrigger")
        {
            StageAnimator = GetComponent<Animator>();
        }
    }

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
                other.gameObject.SendMessage("ToStan");
                StageAnimator.SetTrigger("RouteFalt");
            }
        }
    }
}
