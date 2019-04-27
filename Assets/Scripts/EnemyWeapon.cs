using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float deathDuration = 2.35f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponent<PlayerHealth>().Death(deathDuration);
        }
    }
}
