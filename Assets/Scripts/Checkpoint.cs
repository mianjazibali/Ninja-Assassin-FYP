using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    public Material activeMaterial;

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
            levelManager.lastCheckpointPosition = new Vector3 (respawnPosition.x, respawnPosition.y, levelManager.lastCheckpointPosition.z);
            GetComponent<Renderer>().material = activeMaterial;
            isCheckpointUsed = true;
        }
    }
}
