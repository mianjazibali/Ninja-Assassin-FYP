using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Wall" || other.transform.tag == "Destructible" || other.transform.tag == "Ground")
        {
            //Debug.Log("Wall");
            playerMovement.SetCanMove(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Wall" || other.transform.tag == "Destructible" || other.transform.tag == "Ground")
        {
            playerMovement.SetCanMove(true);
        }
    }
}
