using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;

    public GameObject lifePickupFX;
    private Transform pickupTransform;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        pickupTransform = lifePickupFX.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            GameObject fx = Instantiate(lifePickupFX, pickupTransform.position, pickupTransform.rotation);
            fx.transform.SetParent(player.transform, false);
            playerHealth.IncrementCurrentLives();
            Destroy(gameObject);
        }
    }
}
