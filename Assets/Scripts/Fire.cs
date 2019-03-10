using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject playerFireFX;
    private Transform playerFireTransform;

    private bool isBurning;
    public float respawnDelayTime = 2.35f;

    private void Start()
    {
        playerFireTransform = playerFireFX.transform;
        isBurning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isBurning)
            {
                isBurning = true;
                StartCoroutine(Burn(other.transform.parent.gameObject));
            }
        }
    }

    IEnumerator Burn(GameObject player)
    {
        GameObject fx = Instantiate(playerFireFX, playerFireTransform.position, playerFireTransform.rotation);
        fx.transform.SetParent(player.transform, false);
        player.GetComponent<Animator>().SetTrigger("Burn");
        yield return new WaitForSeconds(respawnDelayTime);
        player.GetComponent<PlayerHealth>().Respawn();
        isBurning = false;
    }
}
