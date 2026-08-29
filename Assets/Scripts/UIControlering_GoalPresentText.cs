using TMPro;
using UnityEngine;

//「Main」シーンにおいて、ルエリアに置かれたキャンバスに表示されるゴール後の一言コメントについて、文章をデータに基づき表示します。
//各キャンバスオブジェクトに付与され、個別管理となります。

public class UIControlering_GoalPresentText : MonoBehaviour
{  
   [SerializeField] private TextMeshProUGUI PresentText;
   private string[] Words;
   private int WordCount;
   private string ThisTimeWord;

   void Start()
   {
      Words = GoalPresentWordData.Instance.PresentWords;
      WordCount = GoalPresentWordData.Instance.WordCount;
      ThisTimeWord = Words[Random.Range(0,WordCount)];
      Debug.Log(ThisTimeWord);

      PresentText.text = ThisTimeWord;
   }
}
