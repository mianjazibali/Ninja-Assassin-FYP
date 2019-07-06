using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Wall" || other.transform.tag == "Destructible")
        {
            //Debug.Log("Wall");
            playerMovement.SetCanMove(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Wall" || other.transform.tag == "Destructible")
        {
            playerMovement.SetCanMove(true);
        }
    }
}
