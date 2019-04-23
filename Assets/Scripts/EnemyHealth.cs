using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float totalHealth;
    public float currentHealth;

    private Animator myAnimator;

    void Start()
    {
        currentHealth = totalHealth;
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void AddDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            GetComponent<EnemyMovement>().DelayedDeath();
        }
    }

}
