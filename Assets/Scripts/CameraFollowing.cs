using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollowing : MonoBehaviour
{   
    //上から順に、対象となるゲームオブジェクト、追っかけるスピード、カメラアングルの修正スピード(対象の回転速度と一致させることを推奨)
    [SerializeField] private GameObject TargetObject;
    [SerializeField] private float FollowSpeed;
    [SerializeField] private float AngleSpeed;

    //対象とカメラの理想的な相対距離
    private Vector3 Distance; //[(m,m,m)]

    //カメラの移動予定位置
    private Vector3 FuturePosition;

    void Start()
    {
        //理想的な距離はシーンに配置した初期位置での相対距離
        Distance = TargetObject.transform.position - transform.position;

        //スタート時は現在位置が移動予定地
        FuturePosition = transform.position;

    }

    void LateUpdate()
    {   
        //移動予定地の更新
        FuturePosition.x = TargetObject.transform.position.x - Distance.z * Mathf.Sin(TargetObject.transform.eulerAngles.y * Mathf.Deg2Rad);
        FuturePosition.z = TargetObject.transform.position.z - Distance.z * Mathf.Cos(TargetObject.transform.eulerAngles.y * Mathf.Deg2Rad);
        FuturePosition.y = TargetObject.transform.position.y - Distance.y;

        //移動予定地に向けて、遠い時は早く、近い時は遅く移動する
        Vector3 pos = Vector3.Lerp(
                                transform.position,
                                FuturePosition,
                                FollowSpeed * Time.deltaTime
                                );
        
        //カメラ位置の更新
        transform.position = pos;

        //カメラアングルの更新
        float Angle_y = TargetObject.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,Angle_y,transform.eulerAngles.z);

    }
}
