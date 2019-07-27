using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    public Material activeMaterial;
    public GameObject activefx;

    private LevelManager levelManager;
    private Vector3 respawnPosition;
    private bool isCheckpointUsed;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        respawnPosition = transform.GetChild(0).position;
        isCheckpointUsed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isCheckpointUsed)
        {
            Instantiate(activefx, new Vector3(transform.position.x, transform.position.y, 3.2f), activefx.transform.rotation);
            levelManager.lastCheckpointPosition = new Vector3 (respawnPosition.x, respawnPosition.y, levelManager.lastCheckpointPosition.z);
            GetComponent<Renderer>().material = activeMaterial;
            isCheckpointUsed = true;
        }
    }
}
