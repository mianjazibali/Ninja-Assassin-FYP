using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{

    public GameObject winMenuUI;
    public GameObject winMenuStars;
    public LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        winMenuStars.transform.GetChild(levelManager.GetScrollsCount()).gameObject.SetActive(true);
        levelManager.SaveScrolls();
        Time.timeScale = 0f;
        winMenuUI.SetActive(true);
    }
}
