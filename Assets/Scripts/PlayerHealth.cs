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
    private Animator myAnimator;

    //Burn Variables
    public bool isBurning = false;

    //Shield Variables
    public GameObject shieldFX;
    public bool isShieldActive = false;

    private void Start()
    {
        currentLives = totalLives;
        currentHealth = totalHealth;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        myAnimator = GetComponent<Animator>();
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

    public void Respawn()
    {
        if(currentLives > 0)
        {
            currentLives--;
            currentHealth = totalHealth;
        }
        if (isShieldActive)
        {
            isShieldActive = false;
            shieldFX.SetActive(isShieldActive);
        }
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

    public void Burn()
    {
        StartCoroutine(BurnIt());
    }

    public IEnumerator BurnIt()
    {
        //GameObject fx = Instantiate(playerFireFX, playerFireTransform.position, playerFireTransform.rotation);
        //fx.transform.SetParent(player.transform, false);
        isBurning = true;
        myAnimator.SetBool("isBurning", isBurning);
        yield return new WaitForSeconds(respawnDelayTime);
        isBurning = false;
        myAnimator.SetBool("isBurning", isBurning);
        Respawn();
    }
}
