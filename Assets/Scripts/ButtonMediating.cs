using UnityEngine;

public class ButtonMediating : MonoBehaviour
{
    public void PlayButtonSE(int index)
    {
        SEManaging.Instance.PlayButtonSE(index);
    }
}
