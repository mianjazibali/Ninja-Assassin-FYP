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

    private bool isBurning;
    public float respawnDelayTime = 2.35f;


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
                if (!isBurning && !rb.GetComponent<PlayerHealth>().isShieldActive)
                {
                    isBurning = true;
                    StartCoroutine(Burn(rb.gameObject));
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
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator Burn(GameObject player)
    {
        //GameObject fx = Instantiate(playerFireFX, playerFireTransform.position, playerFireTransform.rotation);
        //fx.transform.SetParent(player.transform, false);
        player.GetComponent<Animator>().SetBool("isBurning", isBurning);
        yield return new WaitForSeconds(respawnDelayTime);
        isBurning = false;
        player.GetComponent<Animator>().SetBool("isBurning", isBurning);
        player.GetComponent<PlayerHealth>().Respawn();
        Destroy(gameObject);
    }
}
