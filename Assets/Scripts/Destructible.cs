using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    //Health Variables
    public float objectFullHealth = 10f;
    private float objectCurrentHealth;

    //Destructed Version
    public GameObject destroyedVersion;

    void Start()
    {
        objectCurrentHealth = objectFullHealth;
    }

    public void AddDamage(float damage)
    {
        objectCurrentHealth = objectCurrentHealth - damage;
        if (objectCurrentHealth <= 0)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
