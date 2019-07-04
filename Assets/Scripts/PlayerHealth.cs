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
    private Animator myAnimator;
    private PlayerMovement playerMovement;

    //Burn Variables
    public bool isBurning = false;
    public bool isDying = false;

    //Shield Variables
    public GameObject shieldFX;
    public bool isShieldActive = false;
    public GameObject loseMenuUI;

    private void Start()
    {
        currentLives = totalLives;
        currentHealth = totalHealth;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        myAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
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

    private void Reset()
    {
        StopAllCoroutines();
        isShieldActive = false;
        isBurning = false;
        shieldFX.SetActive(false);
    }

    public void Respawn()
    {
        if(currentLives > 1)
        {
            Reset();
            currentLives--;
            currentHealth = totalHealth;
            if (playerMovement.GetPlayerFacing() == -1f)
            {
                playerMovement.FlipPlayer();
            }
            transform.position = levelManager.lastCheckpointPosition;
        }
        else
        {
            loseMenuUI.SetActive(true);
        }
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

    public void Burn(float burnDuration)
    {
        if(!isBurning && !isShieldActive)
            StartCoroutine(BurnCoroutine(burnDuration));
    }

    private IEnumerator BurnCoroutine(float burnDuration)
    {
        //GameObject fx = Instantiate(playerFireFX, playerFireTransform.position, playerFireTransform.rotation);
        //fx.transform.SetParent(player.transform, false);
        isBurning = true;
        myAnimator.SetBool("isBurning", isBurning);
        yield return new WaitForSeconds(burnDuration);
        isBurning = false;
        myAnimator.SetBool("isBurning", isBurning);
        Respawn();
    }

    public void Death(float burnDuration)
    {
        if (!isDying && !isShieldActive)
        {
            transform.tag = "Untagged";
            transform.GetChild(0).tag = "Untagged";
            StartCoroutine(DeathCoroutine(burnDuration));
        }   
    }

    private IEnumerator DeathCoroutine(float burnDuration)
    {
        //GameObject fx = Instantiate(playerFireFX, playerFireTransform.position, playerFireTransform.rotation);
        //fx.transform.SetParent(player.transform, false);
        isDying = true;
        myAnimator.SetBool("isDying", isDying);
        yield return new WaitForSeconds(burnDuration);
        isDying = false;
        myAnimator.SetBool("isDying", isDying);
        transform.tag = "Player";
        transform.GetChild(0).tag = "Player";
        Respawn();
    }
}
