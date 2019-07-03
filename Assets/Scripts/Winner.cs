using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{

    public GameObject winMenuUI;
    public GameObject winMenuStars;
    public LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        int levelReached = SceneManager.GetActiveScene().buildIndex - 6; //1
        if (PlayerPrefs.GetInt("levelReached") < levelReached + 1)
        {
            PlayerPrefs.SetInt("levelReached", levelReached + 1);
            PlayerPrefs.Save();
        }
        winMenuStars.transform.GetChild(levelManager.GetScrollsCount()).gameObject.SetActive(true);
        levelManager.SaveScrolls();
        Time.timeScale = 0f;
        winMenuUI.SetActive(true);
    }
}
