using System;
using UnityEngine;

public class WallClosing : MonoBehaviour
{
    [SerializeField] private StageGenerating stageGenerating;
    
    [SerializeField] private Material material;

    //追いかける速さ、初期のｚ位置
    [SerializeField] private float ClosingSpeed; //[m/s]
    [SerializeField] private float FirstPosition_Z = -40; //[m]

    //壁の高さ、壁の幅、ステージ一個の幅、Scale(1,1)に対するマテリアルのタイリング比
    private float WallHeight = 8.0f; //[m]
    private float WallWidth; //[m]
    private float StageWidth = 16.0f; //[m]
    private float StageLength = 20.0f; //[m]
    private float Tilling = 5; //[]
    
    
    void Start()
    {
        //壁のサイズとポジションをあみだくじの大きさに合わせて調整
        WallWidth = stageGenerating.NumLine * StageWidth;
        transform.localScale = new Vector3(WallWidth,WallHeight,1);
        transform.position = new Vector3(-(WallWidth-StageWidth)/2,WallHeight/2,FirstPosition_Z);
        material.mainTextureScale = new Vector2(WallWidth * Tilling,WallHeight * Tilling);
    }

    void Update()
    {
        if (transform.position.z < stageGenerating.NumRow * StageLength + StageLength /2 - 1.0f)
        {
            //壁を前進させる
            transform.Translate(0,0,ClosingSpeed * Time.deltaTime);
        }
        
    }
}
