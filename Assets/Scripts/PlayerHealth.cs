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

    //Respawn Variables
    public float respawnDelayTime;

    //Reference Variables
    private LevelManager levelManager;

    //Shield Variables
    [SerializeField]
    private bool isShieldActive;

    //FX Variables
    public GameObject bloodSplashFX;

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

    public void AddDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            MakeDead();
        }
    }

    public void MakeDead()
    {
        /*
        if (playerControllerTouch.isGrounded)
        {
            myAnimator.SetTrigger("dead");
        }
        else
        {
        */
            gameObject.SetActive(false);
            Instantiate(bloodSplashFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Invoke("Respawn", respawnDelayTime);
        //}
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
        gameObject.SetActive(true);
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
