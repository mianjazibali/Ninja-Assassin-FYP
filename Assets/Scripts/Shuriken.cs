using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public GameObject hitFX;
    public GameObject bloodSplashFX;
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
        else
        if(other.tag == "Enemy")
        {
            Instantiate(bloodSplashFX, transform.position, transform.rotation);
            other.GetComponent<EnemyHealth>().AddDamage(damagePower);
            Destroy(gameObject);
        }
        else
        if(other.tag == "Shield" || other.tag == "Ground" || other.tag == "Wall")
        {
            GameObject temp = Instantiate(hitFX, transform.position, transform.rotation);
            temp.transform.SetParent(other.transform, true);
            Destroy(gameObject);
        }
    }
}
