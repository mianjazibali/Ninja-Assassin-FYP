using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float damagePower = 10f;
    public float moveSpeed;
    public float rotationSpeed;

    private Rigidbody myRigidbody;

    private Vector3 direction;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = direction * moveSpeed;
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime * rotationSpeed);
    }

    public void setMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void Initialize(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
