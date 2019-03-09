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
    public GameObject shieldFX;
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

    public void AddDamage(float damage)
    {
        if (isShieldActive) return;
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            //MakeDead();
        }
    }

    public void MakeDead()
    {
        /*
        if (playerControllerTouch.isGrounded)
        {
            myAnimator.SetTrigger("dead");
        }
        */
    }

    public void Respawn()
    {
        if(currentLives > 0)
        {
            currentLives--;
            currentHealth = totalHealth;
        }
        shieldFX.SetActive(false);
        transform.position = levelManager.lastCheckpointPosition;
    }

    public void IncrementCurrentLives()
    {
        currentLives++;
    }

    public void SetShield(bool status, float duration)
    {
        isShieldActive = status;
        ParticleSystem ps;
        ps = shieldFX.transform.GetChild(0).GetComponent<ParticleSystem>();
        ps.Stop();
        var main = ps.main;
        main.duration = duration - 2.5f;
        ps.Play();
        shieldFX.SetActive(status);
    }

    public void SetShield(bool status)
    {
        isShieldActive = status;
        shieldFX.SetActive(status);
    }
}
