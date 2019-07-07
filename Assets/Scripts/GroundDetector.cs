using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Ground" || other.transform.tag == "Destructible")
        {
            //Debug.Log("Grounded");
            playerMovement.SetGrounded(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Ground" || other.transform.tag == "Destructible")
        {
            //Debug.Log("Not Grounded");
            playerMovement.SetGrounded(false);
        }
    }
}
