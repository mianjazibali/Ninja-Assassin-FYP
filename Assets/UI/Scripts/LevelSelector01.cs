﻿using UnityEngine;
using UnityEngine.UI;

public class LevelSelector01 : MonoBehaviour
{
    public Button[] levelButtons;
    static int levelReached = 1;

    private void Start()
    {
        if (PlayerPrefs.GetInt("levelReached") == 0)
        {
            PlayerPrefs.SetInt("levelReached", levelReached);
            PlayerPrefs.Save();
        }
        else
        {
            levelReached = PlayerPrefs.GetInt("levelReached");
        }

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].transform.GetChild(1).gameObject.transform.GetChild(PlayerPrefs.GetInt((i + 1).ToString())).gameObject.SetActive(true);
                levelButtons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void Select(int levelIndex)
    {
        Fader.Instance.FadeIn().LoadLevel(levelIndex).FadeOut();
    }

    public static void ResetProgress()
    {
        levelReached = 1;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
