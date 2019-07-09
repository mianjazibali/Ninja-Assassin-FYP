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
    public GameObject[] shurikenUpgradeBars;
    public GameObject[] shurikenUpgradeButtons;
    public GameObject[] dashUpgradeBars;
    public GameObject[] dashUpgradeButtons;

    private int currentLives = 0;
    private int currentShuriken = 0;
    private int currentDash = 0;
    private int coinCount = 0;
    private int defaultLives = 3;
    private int defaultShuriken = 3;
    private int defaultDash = 1;

    private TMPro.TextMeshProUGUI coinsTMPro;

    void Start()
    {
        coinsTMPro = coinsText.GetComponent<TMPro.TextMeshProUGUI>();
        DisplayCurrentCoins();
        currentLives = defaultLives + PlayerPrefs.GetInt("currentLives"); //Default 0 to Add On Existing 3 Default Lives 
        DisplayPreviousUpgradedLives();
        livesUpgradeButtons[0].SetActive(false);
        currentShuriken = defaultShuriken + PlayerPrefs.GetInt("currentShuriken");
        DisplayPreviousUpgradedShuriken();
        shurikenUpgradeButtons[0].SetActive(false);
        currentDash = defaultDash + PlayerPrefs.GetInt("currentDash");
        DisplayPreviousUpgradedDash();
        dashUpgradeButtons[0].SetActive(false);
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

    void DisplayPreviousUpgradedShuriken()
    {
        for (int i = 0; i < shurikenUpgradeBars.Length; i++)
        {
            if (i < currentShuriken)
            {
                shurikenUpgradeBars[i].SetActive(true);
            }
            else
            {
                shurikenUpgradeBars[i].SetActive(false);
            }
        }
        shurikenUpgradeButtons[currentShuriken].SetActive(true);
    }

    void DisplayPreviousUpgradedDash()
    {
        for (int i = 0; i < dashUpgradeBars.Length; i++)
        {
            if (i < currentDash)
            {
                dashUpgradeBars[i].SetActive(true);
            }
            else
            {
                dashUpgradeBars[i].SetActive(false);
            }
        }
        dashUpgradeButtons[currentDash].SetActive(true);
    }

    public void UpgradeLives()
    {
        string costStr = livesUpgradeButtons[currentLives].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
        int cost = Convert.ToInt32(costStr);
        if(coinCount >= cost)
        {
            currentLives++;
            coinCount -= cost;
            PlayerPrefs.SetInt("totalCoins", coinCount);
            PlayerPrefs.SetInt("currentLives", currentLives - defaultLives);
            PlayerPrefs.Save();
        }
        else
        {
            upgradeErrorText.SetActive(true);
        }
        livesUpgradeButtons[currentLives].SetActive(true);
        livesUpgradeBars[currentLives - 1].SetActive(true);
        UpdateCoinText();
        //Debug.Log(PlayerPrefs.GetInt("currentLives"));
    }

    public void UpgradeShuriken()
    {
        string costStr = shurikenUpgradeButtons[currentShuriken].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
        int cost = Convert.ToInt32(costStr);
        if (PlayerPrefs.GetInt("totalCoins") >= cost)
        {
            currentShuriken++;
            coinCount -= cost;
            PlayerPrefs.SetInt("totalCoins", coinCount);
            PlayerPrefs.SetInt("currentDash", currentShuriken - defaultShuriken);
            PlayerPrefs.Save();
        }
        else
        {
            upgradeErrorText.SetActive(true);
        }
        shurikenUpgradeButtons[currentDash].SetActive(true);
        shurikenUpgradeBars[currentDash - 1].SetActive(true);
        UpdateCoinText();
        //Debug.Log(PlayerPrefs.GetInt("currentDash"));
    }

    public void UpgradeDash()
    {
        string costStr = dashUpgradeButtons[currentDash].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
        int cost = Convert.ToInt32(costStr);
        if (PlayerPrefs.GetInt("totalCoins") >= cost)
        {
            currentDash++;
            coinCount -= cost;
            PlayerPrefs.SetInt("totalCoins", coinCount);
            PlayerPrefs.SetInt("currentDash", currentDash - defaultDash);
            PlayerPrefs.Save();
        }
        else
        {
            upgradeErrorText.SetActive(true);
        }
        dashUpgradeButtons[currentDash].SetActive(true);
        dashUpgradeBars[currentDash - 1].SetActive(true);
        UpdateCoinText();
        //Debug.Log(PlayerPrefs.GetInt("currentDash"));
    }
}
