using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        if (Music.isMusicOn == 0)
        {
            GetComponent<Toggle>().isOn = true;
        }
        else
        {
            GetComponent<Toggle>().isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleMusic()
    {
        bool toggleState = GetComponent<Toggle>().isOn;
        if (toggleState)
        {
            Music.isMusicOn = 0;
        }
        else
        {
            Music.isMusicOn = 1;
        }
    }
}
