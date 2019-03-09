using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerHealth playerHealth;

    public GameObject lifePickupFX;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(lifePickupFX, transform.position, lifePickupFX.transform.rotation);
            playerHealth.IncrementCurrentLives();
            Destroy(gameObject);
        }
    }
}
