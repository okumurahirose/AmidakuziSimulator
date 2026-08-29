using TMPro;
using UnityEngine;
using UnityEngine.UI;

//UIにおいて、シリンダーが返す値をプレイヤーが見えるようテキストに起こします。

public class Slider_TextAdjusting : MonoBehaviour
{
    private Slider MySlider;
    [SerializeField] private TextMeshProUGUI NumberBoard;

    void Start()
    {
        //自身のスライダーコンポーネントの取得
        MySlider = GetComponent<Slider>();
    }

    void Update()
    {   
        NumberBoard.text = MySlider.value.ToString();
    }

    public void RandomGenerate()
    {
        MySlider.value = Random.Range(MySlider.minValue,MySlider.maxValue);
    }
}
