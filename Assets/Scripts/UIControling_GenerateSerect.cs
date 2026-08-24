using UnityEngine;

public class UIControling_GenerateSerect : MonoBehaviour
{
    public void PassingSetting_NumLine(float value)
    {
        AmidakuziGenerateSetting.Instance.NumLine = (int)value;
    }

    public void PassingSetting_NumRow(float value)
    {
        AmidakuziGenerateSetting.Instance.NumRow = (int)value;
    }

    public void PassingSetting_CornerRate(float value)
    {
        AmidakuziGenerateSetting.Instance.CornerRate = value;
    }
}
