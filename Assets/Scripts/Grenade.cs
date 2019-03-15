using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;

    public GameObject explosionFX;

    float countdown;
    public bool hasExploded = false;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionFX, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if(dest != null)
            {
                dest.Destroy();
            }
        }
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = null;
            if (nearbyObject.CompareTag("Player"))
            {
                rb = nearbyObject.transform.parent.GetComponent<Rigidbody>();
                PlayerHealth playerHealth = rb.GetComponent<PlayerHealth>();
                if (!playerHealth.isShieldActive && !playerHealth.isBurning)
                {
                    playerHealth.Burn();
                }
            }
            else
            if(nearbyObject.CompareTag("Destructible"))
            {
                rb = nearbyObject.GetComponent<Rigidbody>();
            }

            if(rb != null)
            {
                rb.AddExplosionForce(force * rb.mass, transform.position, radius);
            }
        }
        Destroy(gameObject);
    }
}
