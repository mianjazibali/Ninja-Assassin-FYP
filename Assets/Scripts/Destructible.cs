using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    //Health Variables
    public float objectFullHealth = 10f;
    private float objectCurrentHealth;

    //Destructed Version
    public GameObject destroyedVersion;
    public GameObject reward;
    public float rewardForce = 10f;

    GameObject player;
    Transform rewardTransform;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objectCurrentHealth = objectFullHealth;
        rewardTransform = reward.transform;
    }

    public void AddDamage(float damage)
    {
        objectCurrentHealth = objectCurrentHealth - damage;
        if (objectCurrentHealth <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized; 
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        GameObject obj = Instantiate(reward, new Vector3(transform.position.x, transform.position.y, rewardTransform.position.z), rewardTransform.rotation);
        obj.GetComponent<Rigidbody>().AddForce(direction * -rewardForce, ForceMode.Impulse);
        Destroy(gameObject);
    }
}
