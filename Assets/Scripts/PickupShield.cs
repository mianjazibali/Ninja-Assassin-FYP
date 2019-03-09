using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupShield : MonoBehaviour
{
    private GameObject player;
    private LevelManager levelManager;
    private PlayerHealth playerHealth;

    [SerializeField]
    private float shieldDuration = 0;

    public GameObject shieldPickupFX;
    private Transform pickupTransform;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerHealth = player.GetComponent<PlayerHealth>();
        pickupTransform = shieldPickupFX.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Pick());
        }
    }

    IEnumerator Pick()
    {
        GameObject fx = Instantiate(shieldPickupFX, pickupTransform.position, pickupTransform.rotation);
        fx.transform.SetParent(player.transform, false);
        playerHealth.SetShield(true, shieldDuration);
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(shieldDuration);
        playerHealth.SetShield(false);
        Destroy(gameObject);
    }
}
