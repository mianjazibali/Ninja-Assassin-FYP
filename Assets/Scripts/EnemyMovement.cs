using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 10f;
    public float stayDuration = 3f;
    public float attackDuration = 3f;

    private Rigidbody myRigidbody;
    private Animator myAnimator;

    private bool facingRight = true;
    private float moveInput = 1;
    private bool isLooking = false;
    private bool isAttacking = false;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        myAnimator.SetFloat("moveSpeed", Mathf.Abs(myRigidbody.velocity.x));
        if (transform.localPosition.x >= pointB.localPosition.x && facingRight && !isLooking)
        {
            StartCoroutine(DelayedFlip());
        }
        else
        if (transform.localPosition.x <= pointA.localPosition.x && !facingRight && !isLooking)
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
        isLooking = true;
        float temp = moveInput;
        moveInput = 0f;
        myRigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(stayDuration);
        facingRight = !facingRight;
        Vector3 originalScale = transform.localScale;
        originalScale.z = -1 * originalScale.z;
        transform.localScale = originalScale;
        moveInput = temp * -1f;
        isLooking = false;
    }

    public void InstantFlip()
    {
        StopAllCoroutines();
        facingRight = !facingRight;
        Vector3 originalScale = transform.localScale;
        originalScale.z = -1 * originalScale.z;
        transform.localScale = originalScale;
        if (facingRight) moveInput = 1f;
        else moveInput = -1f;
        isLooking = false;
    }

    public void InstantAttack()
    {
        if (!isAttacking)
            StartCoroutine(DelayedAttack());
    }

    IEnumerator DelayedAttack()
    {
        isAttacking = true;
        float temp = moveInput;
        moveInput = 0f;
        myRigidbody.velocity = Vector3.zero;
        myAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);
        moveInput = temp;
        isAttacking = false;
    }
}
