using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Vector3 lastCheckpointPosition;

    public GameObject coinsText;
    public GameObject scrollText;

    private int scrollCount;
    private int coinCount;

    private TMPro.TextMeshProUGUI coinTMPro;

    private void Start()
    {
        lastCheckpointPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        coinTMPro = coinsText.GetComponent<TMPro.TextMeshProUGUI>();
        if (PlayerPrefs.GetInt("totalCoins") == 0)
        {
            PlayerPrefs.SetInt("totalCoins", coinCount);
            PlayerPrefs.Save();
        }
        else
        {
            coinCount = PlayerPrefs.GetInt("totalCoins");
        }
        coinTMPro.text = coinCount.ToString("D6");
    }

    public void IncrementScroll()
    {
        scrollCount++;
    }

    public void SetCoins(int coins)
    {
        coinCount = coinCount + coins;
        SaveCoins();
    }

    public void SaveCoins()
    {
        coinTMPro.text = coinCount.ToString("D6");
        PlayerPrefs.SetInt("totalCoins", coinCount);
        PlayerPrefs.Save();
    }
}
