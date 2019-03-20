using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public GameObject bloodSplashFX;

    public float respawnDelayTime = 1.2f;

    GameObject player;
    PlayerHealth playerHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !playerHealth.isShieldActive)
        {
            player.SetActive(false);
            StartCoroutine(Respawn(player));
        }
    }

    IEnumerator Respawn(GameObject player)
    {
        Instantiate(bloodSplashFX, player.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        yield return new WaitForSeconds(respawnDelayTime);
        playerHealth.Respawn();
        player.SetActive(true);
    }
}
