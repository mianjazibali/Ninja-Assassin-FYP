using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack01 : MonoBehaviour
{
    public bool flip = false;
    public bool attack = false;

    EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (flip)
        {
            enemyMovement.InstantFlip();
        }
        
        if (attack)
        {
            enemyMovement.Attack();
        }
    }
}
