using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float damagePower = 10f;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destructible")
        {
            other.GetComponent<Destructible>().AddDamage(damagePower);
            Destroy(gameObject);
        }
    }
}
