using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damagePower = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destructible")
        {
            other.GetComponent<Destructible>().AddDamage(damagePower);
        }
    }
}
