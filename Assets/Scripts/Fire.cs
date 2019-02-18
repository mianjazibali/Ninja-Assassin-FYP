using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject playerFireFX;

    public float respawnDelayTime = 2.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.movementAllowed = false;
            StartCoroutine(Respawn(other.gameObject));
        }
    }

    IEnumerator Respawn(GameObject player)
    {
        player.GetComponent<Animator>().SetTrigger("Burn");
        Transform prefabTransform = playerFireFX.transform;
        GameObject fire = Instantiate(playerFireFX, new Vector3(player.transform.position.x, prefabTransform.position.y, prefabTransform.position.z) , prefabTransform.rotation);
        fire.transform.parent = player.transform;
        yield return new WaitForSeconds(respawnDelayTime);
        Destroy(fire);
        player.GetComponent<PlayerHealth>().Respawn();
        playerMovement.movementAllowed = true;
    }
}
