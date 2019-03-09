using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupShield : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerHealth playerHealth;

    [SerializeField]
    private float shieldDuration = 0;

    public GameObject shieldPickupFX;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
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
        Instantiate(shieldPickupFX, transform.position, shieldPickupFX.transform.rotation);
        playerHealth.SetShield(true, shieldDuration);
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(shieldDuration);
        playerHealth.SetShield(false);
        Destroy(gameObject);
    }
}
