using UnityEngine;

public class AmidakuziGenerateSettingMediating : MonoBehaviour
{
    public void PassingSetting_PlayMode_Nomal()
    {
        AmidakuziGenerateSetting.Instance.PlayMode = AmidakuziGenerateSetting.Enum_PlayMode.Nomal;
    }

    public void PassingSetting_PlayMode_Endless()
    {
        AmidakuziGenerateSetting.Instance.PlayMode = AmidakuziGenerateSetting.Enum_PlayMode.Endless;
    }

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
