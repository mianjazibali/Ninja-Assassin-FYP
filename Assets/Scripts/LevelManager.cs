using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Vector3 lastCheckpointPosition;

    private void Start()
    {
        lastCheckpointPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
