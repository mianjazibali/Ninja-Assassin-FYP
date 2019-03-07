using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            levelManager.lastCheckpointPosition = new Vector3 (transform.position.x, levelManager.lastCheckpointPosition.y, levelManager.lastCheckpointPosition.z);
        }
    }
}
