using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerHealth playerHealth;

    private enum CollectibleType { Scroll, Coins, Life, Shield };
    [SerializeField]
    private CollectibleType collectibleType = CollectibleType.Scroll;

    [SerializeField]
    private int coinsCount = 0;
    [SerializeField]
    private float shieldDuration = 0;

    public GameObject scrollPickupFX;
    public GameObject coinsPickupFX;
    public GameObject lifePickupFX;
    public GameObject shieldPickupFX;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (collectibleType == CollectibleType.Scroll)
            {
                Instantiate(scrollPickupFX, transform.position, transform.rotation);
                levelManager.IncrementScroll();
                Destroy(gameObject);
            }
            else
            if (collectibleType == CollectibleType.Coins)
            {
                Instantiate(coinsPickupFX, transform.position, transform.rotation);
                levelManager.SetCoins(coinsCount);
                Destroy(gameObject);
            }
            else
            if (collectibleType == CollectibleType.Life)
            {
                Instantiate(lifePickupFX, transform.position, transform.rotation);
                playerHealth.IncrementCurrentLives();
                Destroy(gameObject);
            }
            else
            if (collectibleType == CollectibleType.Shield)
            {
                StartCoroutine(PickupShield());
            }
        }
    }

    IEnumerator PickupShield()
    {
        Instantiate(shieldPickupFX, transform.position, transform.rotation);
        playerHealth.SetShield(true, shieldDuration);
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(shieldDuration);
        playerHealth.SetShield(false);
        Destroy(gameObject);
    }
}
