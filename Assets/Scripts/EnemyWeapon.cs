using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float deathDuration = 2.35f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject player = other.transform.parent.gameObject;
            if (!player.GetComponent<PlayerMovement>().isDashing())
            {
                player.GetComponent<PlayerHealth>().Death(deathDuration);
            }
        }
    }
}
