using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float burnDuration = 2.35f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponent<PlayerHealth>().Burn(burnDuration);
        }
    }   
}
