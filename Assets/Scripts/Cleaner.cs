using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    public GameObject bloodSplashFX;

    public float respawnDelayTime = 1.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject player = other.transform.parent.gameObject;
            player.SetActive(false);
            StartCoroutine(Respawn(player));
        }
    }

    IEnumerator Respawn(GameObject player)
    {
        Instantiate(bloodSplashFX, player.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        yield return new WaitForSeconds(respawnDelayTime);
        PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth>();
        playerHealth.Respawn();
        player.SetActive(true);
    }
}
