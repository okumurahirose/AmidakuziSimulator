using TMPro;
using UnityEngine;

public class UIControlering_GoalPresentText : MonoBehaviour
{  
   [SerializeField] private TextMeshProUGUI PresentText;
   private string[] Words;
   private int WordCount;
   private string ThisTimeWord;

   void Start()
   {
      Words = GoalPresentTextData.Instance.PresentWords;
      WordCount = GoalPresentTextData.Instance.WordCount;
      ThisTimeWord = Words[Random.Range(0,WordCount)];
      Debug.Log(ThisTimeWord);

      PresentText.text = ThisTimeWord;
   }
}
