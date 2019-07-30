using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    void Awake()
    {
        if (AudioManager.isSoundOn == 0)
        {
            GetComponent<Toggle>().isOn = true;
        }
        else
        {
            GetComponent<Toggle>().isOn = false;
        }
    }

    public void toggleSound()
    {
        bool toggleState = GetComponent<Toggle>().isOn;
        if (toggleState)
        {
            AudioManager.isSoundOn = 0;
        }
        else
        {
            AudioManager.isSoundOn = 1;
        }
        AudioManager.Save();
    }
}
