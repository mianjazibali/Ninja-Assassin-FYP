using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    int levelReached;

    void Awake()
    {
        levelReached = PlayerPrefs.GetInt("levelReached");

        if (levelReached >= 19)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(-1100, 0, 0);
        }
        else
        if (levelReached >= 10)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(-550, 0, 0);
        }
        else
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
        
    }
}
