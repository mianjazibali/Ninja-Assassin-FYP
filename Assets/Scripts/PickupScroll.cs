using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScroll : MonoBehaviour
{
    private GameObject player;
    private LevelManager levelManager;

    public GameObject scrollPickupFX;
    private Transform pickupTransform;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        pickupTransform = scrollPickupFX.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject fx = Instantiate(scrollPickupFX, pickupTransform.position, pickupTransform.rotation);
            fx.transform.SetParent(player.transform, false);
            levelManager.IncrementScroll();
            Destroy(gameObject);
        }
    }
}
