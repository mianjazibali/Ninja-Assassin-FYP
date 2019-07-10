using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour
{
    private GameObject player;
    private LevelManager levelManager;

    [SerializeField]
    private int coinsCount = 0;

    public GameObject coinsPickupFX;
    private Transform pickupTransform;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        pickupTransform = coinsPickupFX.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            GameObject fx = Instantiate(coinsPickupFX, pickupTransform.position, pickupTransform.rotation);
            fx.transform.SetParent(player.transform, false);
            levelManager.SetCoins(coinsCount);
            Destroy(gameObject);
        }
    }
}
