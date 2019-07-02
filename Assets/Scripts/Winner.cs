using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{

    public GameObject winMenuUI;

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0f;
        winMenuUI.SetActive(true);
    }
}
