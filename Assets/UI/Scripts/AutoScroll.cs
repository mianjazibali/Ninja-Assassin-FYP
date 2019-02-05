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
            GetComponent<RectTransform>().localPosition = new Vector3(-1 * gameObject.transform.GetChild(2).gameObject.transform.position.x - 100, 0, 0);
        }
        else
        if (levelReached >= 10)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(-1 * gameObject.transform.GetChild(1).gameObject.transform.position.x - 100, 0, 0);
        }
        else
        {
            GetComponent<RectTransform>().localPosition = new Vector3(gameObject.transform.GetChild(0).gameObject.transform.position.x - 100, 0, 0);
        }
        
    }
}
