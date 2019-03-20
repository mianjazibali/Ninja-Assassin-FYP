using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerHealth playerHealth;

    [SerializeField]
    private int coinsCount = 0;

    public GameObject coinsPickupFX;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            Instantiate(coinsPickupFX, transform.position, coinsPickupFX.transform.rotation);
            levelManager.SetCoins(coinsCount);
            Destroy(gameObject);
        }
    }
}
