using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 10f;
    public float stayDuration = 3f;

    private Rigidbody myRigidbody;
    private Animator myAnimator;

    private bool facingRight = true;
    private float moveInput = 1;
    private bool isCoroutineRunning = false;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        myAnimator.SetFloat("moveSpeed", Mathf.Abs(myRigidbody.velocity.x));
        if(transform.localPosition.x >= pointB.localPosition.x && facingRight && !isCoroutineRunning)
        {
            StartCoroutine(DelayedFlip());
        }
        else
        if(transform.localPosition.x <= pointA.localPosition.x && !facingRight && !isCoroutineRunning)
        {
            StartCoroutine(DelayedFlip());
        }
        
        if (facingRight)
        {
            myRigidbody.velocity = new Vector3(moveInput * moveSpeed, myRigidbody.velocity.y, 0);
        }
        else
        {
            myRigidbody.velocity = new Vector3(moveInput * moveSpeed, myRigidbody.velocity.y, 0);
        }
        
    }

    IEnumerator DelayedFlip()
    {
        isCoroutineRunning = true;
        float temp = moveInput;
        moveInput = 0f;
        myRigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(stayDuration);
        facingRight = !facingRight;
        Vector3 originalScale = transform.localScale;
        originalScale.z = -1 * originalScale.z;
        transform.localScale = originalScale;
        moveInput = temp * -1f;
        isCoroutineRunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(DelayedFlip());
            InstantFlip();
        }
    }

    void InstantFlip()
    {
        isCoroutineRunning = true;
        StopAllCoroutines();
        facingRight = !facingRight;
        Vector3 originalScale = transform.localScale;
        originalScale.z = -1 * originalScale.z;
        transform.localScale = originalScale;
        if (facingRight) moveInput = 1f;
        else moveInput = -1f;
        isCoroutineRunning = false;
    }
}
