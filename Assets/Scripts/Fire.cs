using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public GameObject playerFireFX;
    private Transform playerFireTransform;

    private bool isBurning;
    public float respawnDelayTime = 2.35f;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerFireTransform = playerFireFX.transform;
        isBurning = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !playerHealth.isShieldActive)
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
        //GameObject fx = Instantiate(playerFireFX, playerFireTransform.position, playerFireTransform.rotation);
        //fx.transform.SetParent(player.transform, false);
        player.GetComponent<Animator>().SetBool("isBurning", isBurning);
        yield return new WaitForSeconds(respawnDelayTime);
        isBurning = false;
        player.GetComponent<Animator>().SetBool("isBurning", isBurning);
        player.GetComponent<PlayerHealth>().Respawn();
    }
}
