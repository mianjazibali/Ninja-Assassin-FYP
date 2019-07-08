using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject coinsText;
    public GameObject upgradeErrorText;
    public GameObject[] livesUpgradeBars;
    public GameObject[] livesUpgradeButtons;

    private int currentLives = 0;
    private int coinCount = 0;
    private int defaultLives = 3;

    private TMPro.TextMeshProUGUI coinsTMPro;

    void Start()
    {
        coinsTMPro = coinsText.GetComponent<TMPro.TextMeshProUGUI>();
        DisplayCurrentCoins();
        currentLives = defaultLives + PlayerPrefs.GetInt("currentLives"); //Default 0 to Add On Existing 3 Default Lives 
        DisplayPreviousUpgradedLives();
        livesUpgradeButtons[0].SetActive(false);
    }

    void UpdateCoinText()
    {
        coinsTMPro.text = coinCount.ToString("D6");
    }

    void DisplayCurrentCoins()
    {
        if (PlayerPrefs.GetInt("totalCoins") == 0)
        {
            PlayerPrefs.SetInt("totalCoins", coinCount);
            PlayerPrefs.Save();
        }
        else
        {
            coinCount = PlayerPrefs.GetInt("totalCoins");
        }
        UpdateCoinText();
    }

    void DisplayPreviousUpgradedLives()
    {
        for (int i = 0; i < livesUpgradeBars.Length; i++)
        {
            if(i < currentLives)
            {
                livesUpgradeBars[i].SetActive(true);
            }
            else
            {
                livesUpgradeBars[i].SetActive(false);
            }
        }
        livesUpgradeButtons[currentLives].SetActive(true);
    }

    public void UpgradeLives()
    {
        string costStr = livesUpgradeButtons[currentLives].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
        int cost = Convert.ToInt32(costStr);
        if(PlayerPrefs.GetInt("totalCoins") >= cost)
        {
            currentLives++;
            PlayerPrefs.SetInt("currentLives", currentLives - defaultLives);
            PlayerPrefs.Save();
        }
        else
        {
            upgradeErrorText.SetActive(true);
        }
        livesUpgradeButtons[currentLives].SetActive(true);
        livesUpgradeBars[currentLives - 1].SetActive(true);
        //Debug.Log(PlayerPrefs.GetInt("currentLives"));
    }
}
