using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScroll : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerHealth playerHealth;

    public GameObject scrollPickupFX;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(scrollPickupFX, transform.position, scrollPickupFX.transform.rotation);
            levelManager.IncrementScroll();
            Destroy(gameObject);
        }
    }
}
