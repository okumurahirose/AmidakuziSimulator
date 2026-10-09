using UnityEngine;
using UnityEngine.UI;
public class ToggleMediating : MonoBehaviour
{   
    public void BGMmute()
    {   
        BGMManaging.Instance.gameObject.GetComponent<AudioSource>().mute = !GetComponent<Toggle>().isOn;
    }

    public void SEmute()
    {
        SEManaging.Instance.gameObject.GetComponent<AudioSource>().mute = !GetComponent<Toggle>().isOn;
    }
}
