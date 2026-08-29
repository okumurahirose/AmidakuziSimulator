using TMPro;
using UnityEngine;

//「SettimgMenu」シーン中において、「Main」シーンのゴールエリアに置かれたキャンバスに表示されるゴール後の一言コメントについて、その文章の入力パネルの情報を管理、フラグ送信を制御します。
//各入力パネルオブジェクトに付与され、個別管理となります。

public class PresentWordInputFieldPanel : MonoBehaviour
{
    public UIConroling_SettingMenu_PresentWord UIConroling;

    //インプットフィールドに入力された文章、インプットフィールドの番号
    public TMP_InputField Word;
    public TextMeshProUGUI NumberText;

    //パネルの位置
    private RectTransform rectTransform;

    void Start()
    {   
        foreach(Transform target in GetComponentInChildren<Transform>())
        {
            if(target.gameObject.tag == "InputField")
            {
                Word = target.gameObject.GetComponent<TMP_InputField>();
            }
            if(target.gameObject.tag == "InputFieldNumber")
            {
                NumberText = target.gameObject.GetComponent<TextMeshProUGUI>();
            }
        }

        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void PassingData()
    {   
        int Num = int.Parse(NumberText.text);

        if(Word.text.Length == 0)
        {   
                UIConroling.DeletePanel(gameObject,Num);
        }
        else 
        {
            UIConroling.RegisterWords(Word,rectTransform,Num);    
        }
        
    }
}
