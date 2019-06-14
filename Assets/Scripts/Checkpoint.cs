using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager levelManager;
    public Material activeMaterial;
    private bool isCheckpointUsed;

    private void Start()
    {
        isCheckpointUsed = false;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isCheckpointUsed)
        {
            levelManager.lastCheckpointPosition = new Vector3 (transform.position.x, levelManager.lastCheckpointPosition.y, levelManager.lastCheckpointPosition.z);
            GetComponent<Renderer>().material = activeMaterial;
            isCheckpointUsed = true;
        }
    }
}
