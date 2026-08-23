using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
