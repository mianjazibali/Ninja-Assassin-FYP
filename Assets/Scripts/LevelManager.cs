using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Vector3 lastCheckpointPosition;

    [SerializeField]
    private int scrollCount = 0;
    [SerializeField]
    private int coinCount;

    private void Start()
    {
        lastCheckpointPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void IncrementScroll()
    {
        scrollCount++;
    }

    public void SetCoins(int coins)
    {
        coinCount = coinCount + coins;
    }
}
