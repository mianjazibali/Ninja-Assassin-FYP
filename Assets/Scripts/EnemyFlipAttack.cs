using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlipAttack : MonoBehaviour
{
    public bool flip = false;
    public bool attack = false;
    public bool flipOnHit = false;

    EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (flip)
            {
                enemyMovement.InstantFlip();
            }

            if (attack)
            {
                enemyMovement.InstantAttack();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PlayerWeapon") && flipOnHit)
        {
            enemyMovement.InstantFlip();
        }
    }
}
