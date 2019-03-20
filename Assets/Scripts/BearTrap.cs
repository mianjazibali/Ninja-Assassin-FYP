using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public float damage = 10f;
    public float damageRate = 1f;
    public float trappedTime = 3f;

    float nextDamage;

    PlayerHealth playerHealth;
    Movement playerMovement;
    bool isTrapped = false;

    void Start()
    {
        nextDamage = Time.time;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<Movement>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrapped)
        {
            isTrapped = true;
            playerMovement.BlockMovement();
            playerMovement.Attack();
            playerHealth.GetComponent<Animator>().SetBool("Trapped", isTrapped);
            StartCoroutine(DestroyTrap());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isTrapped && nextDamage <= Time.time)
        {
            playerHealth.AddDamage(damage);
            nextDamage = Time.time + damageRate;
        }
    }

    IEnumerator DestroyTrap()
    {
        yield return new WaitForSeconds(trappedTime - 1);
        isTrapped = false;
        playerHealth.GetComponent<Animator>().SetBool("Trapped", isTrapped);
        Destroy(gameObject);
    }
}
