using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{   
    //上から順に、対象となるゲームオブジェクト、追っかけるスピード
    [SerializeField] private GameObject TargetObject;
    [SerializeField] private float FollowSpeed;

    //対象とカメラの理想的な距離
    private Vector3 Distance; //[(m,m,m,)]

    void Start()
    {
        //理想的な距離はシーンに配置した初期位置での距離
        Distance = TargetObject.transform.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {   
        //距離が離れている時は早く、近い時は遅く近づく
        Vector3 pos = Vector3.Lerp(
                        transform.position,
                        TargetObject.transform.position - Distance,
                        FollowSpeed * Time.deltaTime);

        //カメラ位置の更新
        transform.position = pos;

    }
}
