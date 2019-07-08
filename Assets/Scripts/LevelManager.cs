using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Vector3 lastCheckpointPosition;

    public GameObject coinsText;
    public GameObject scrollText;
    public GameObject livesText;

    private int scrollCount;
    private int coinCount;

    private TMPro.TextMeshProUGUI coinsTMPro;
    private TMPro.TextMeshProUGUI scrollsTMPro;
    

    private void Start()
    {
        lastCheckpointPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        coinsTMPro = coinsText.GetComponent<TMPro.TextMeshProUGUI>();
        if (PlayerPrefs.GetInt("totalCoins") == 0)
        {
            PlayerPrefs.SetInt("totalCoins", coinCount);
            PlayerPrefs.Save();
        }
        else
        {
            coinCount = PlayerPrefs.GetInt("totalCoins");
        }
        coinsTMPro.text = coinCount.ToString("D6");
        scrollsTMPro = scrollText.GetComponent<TMPro.TextMeshProUGUI>();
        scrollsTMPro.text = scrollCount.ToString();
    }

    public int GetScrollsCount()
    {
        return scrollCount;
    }

    public void IncrementScroll()
    {
        scrollCount++;
        scrollsTMPro.text = scrollCount.ToString();
    }

    public void SetCoins(int coins)
    {
        coinCount = coinCount + coins;
        SaveCoins();
    }

    public void SaveCoins()
    {
        coinsTMPro.text = coinCount.ToString("D6");
        PlayerPrefs.SetInt("totalCoins", coinCount);
        PlayerPrefs.Save();
    }

    public void SaveScrolls()
    {
        int level = SceneManager.GetActiveScene().buildIndex - 6;
        if (PlayerPrefs.GetInt(level.ToString()) == 0)
        {
            PlayerPrefs.SetInt(level.ToString(), scrollCount);
            PlayerPrefs.Save();
        }
        else
        {
            int savedScrolls = PlayerPrefs.GetInt(level.ToString());
            if(savedScrolls < scrollCount)
            {
                PlayerPrefs.SetInt(level.ToString(), scrollCount);
                PlayerPrefs.Save();
            }
        }
    }
}
