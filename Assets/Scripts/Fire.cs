using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;

    public float burnDuration = 2.35f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !playerHealth.isShieldActive && !playerHealth.isBurning)
        {
            playerHealth.Burn(burnDuration);
        }
    }   
}
