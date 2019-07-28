using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateSpikes : MonoBehaviour
{
    private List<GameObject> spikes = new List<GameObject>();
    
    void Start()
    {
        foreach (Transform child in transform)
        {
            spikes.Add(child.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject spike in spikes)
            {
                spike.GetComponent<MoveY>().enabled = true;
            }
        }
    }
}
