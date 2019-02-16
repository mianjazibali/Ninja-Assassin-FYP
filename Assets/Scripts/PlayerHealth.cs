using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Health Variables
    public int totalLives;
    public int currentLives;
    public float totalHealth;
    public float currentHealth;

    //Reference Variables
    private LevelManager levelManager;

    //Shield Variables
    [SerializeField]
    private bool isShieldActive;

    private void Start()
    {
        currentLives = totalLives;
        currentHealth = totalHealth;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        /*
        if(currentLives > 0)
        {
            currentLives--;
            currentHealth = totalHealth;
        }
        */
        transform.position = levelManager.lastCheckpointPosition;
    }

    public void IncrementCurrentLives()
    {
        currentLives++;
    }

    public void SetShield(bool status)
    {
        isShieldActive = status; 
    }
}
