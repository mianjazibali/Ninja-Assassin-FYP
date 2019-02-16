using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestroyObject : MonoBehaviour {

    public float objectFullHealth = 10f;
    private float objectCurrentHealth;

    public GameObject destroyedVersion;

	// Use this for initialization
	void Start () {
        objectCurrentHealth = objectFullHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addDamageToObject(float damage)
    {
        objectCurrentHealth = objectCurrentHealth - damage;
        if (objectCurrentHealth <= 0)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Shuriken")
        {
            addDamageToObject(other.GetComponent<Shuriken>().damagePower);
            Destroy(other.gameObject);
        }
        else
        if(other.name == "Sword")
        {
            //addDamageToObject(other.GetComponent<Sword>().damagePower);
        }
    }
}
