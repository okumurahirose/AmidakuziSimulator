using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    CharacterController characterController;

    //上から順に、最大速度、角速度、加速度、重力
    [SerializeField] private float MaxMovingSpeed; //[m/s]
    public float RotationSpeed; //[deg/s] （CameraFollowingで参照する）
    [SerializeField] private float AccelerateSpeed; //[m/s^2]
    private const float Glavity = -9.81f;

    //上から順に、現在の速度、移動距離、現在位置
    private float MovingSpeed; //[m/s]
    private Vector3 MoveDirection; //[(m,m,m)]
    private Vector3 CurrentPositon; //[(m,m,m)]


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //コンポーネントの取得と各種変数の初期化
        characterController = GetComponent<CharacterController>();
        MovingSpeed = 0.0f;
        MoveDirection = Vector3.zero;
        CurrentPositon = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        //現在位置の更新
        CurrentPositon = transform.position;

        //設置している時のみ移動
        if(characterController.isGrounded){
            
            //設置しているため、重力によるy軸への影響はない
            MoveDirection.y = 0;

            //回転
            if (Keyboard.current.aKey.isPressed)
            {
                transform.Rotate(0,-RotationSpeed * Time.deltaTime,0);
            }
            if (Keyboard.current.dKey.isPressed)
            {
                transform.Rotate(0,RotationSpeed * Time.deltaTime,0);
            }

            //移動
            MovingSpeed += AccelerateSpeed * Time.deltaTime;
            MovingSpeed = Mathf.Clamp(MovingSpeed,0,MaxMovingSpeed);
            MoveDirection.z = MovingSpeed * Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
            MoveDirection.x = MovingSpeed * Time.deltaTime * Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
            
        }
        else
        {
            MoveDirection.y += Glavity * Time.deltaTime * Time.deltaTime / 2;
        }
        
        //移動の実施
        characterController.Move(MoveDirection);
    }
}
