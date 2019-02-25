﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Animator myAnimator;

    //Running Variables
    bool movementAllowed;
    public float runSpeed = 15f;
    bool facingRight;

    //Jumping Variables
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpForce;
    public float fallMultiplier;
    public float jumpMultiplier;

    //Shuriken variables 
    public GameObject shurikenPrefab;
    public Transform shurikenPosition;
    private float throwSpeed = 1000f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        //movementAllowed = true;
        facingRight = true;
    }

    private void FixedUpdate()
    {
        myAnimator.SetFloat("verticalSpeed", myRigidbody.velocity.y);
        if (grounded && CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            grounded = false;
            myAnimator.SetTrigger("Jump");
            myRigidbody.velocity = Vector3.up * jumpForce;
        }
        if(myRigidbody.velocity.y < 0)
        {
            myRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else
        {
            myRigidbody.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;
        myAnimator.SetBool("isGrounded", grounded);

        if (movementAllowed)
        {
            float move = CrossPlatformInputManager.GetAxis("Horizontal");
            myAnimator.SetFloat("moveSpeed", Mathf.Abs(move));
            myRigidbody.velocity = new Vector3(move * runSpeed, myRigidbody.velocity.y, 0);
            if (move > 0 && !facingRight) FlipPlayer();
            else if (move < 0 && facingRight) FlipPlayer();
        }

        //Shuriken
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            myAnimator.SetTrigger("Shuriken");
        }

        //Sword
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            //myAnimator.SetTrigger("Shuriken");
        }

        //Disguise
        if (CrossPlatformInputManager.GetButtonDown("Fire3"))
        {
            //myAnimator.SetTrigger("Shuriken");
        }
    }

    public void AllowMovement()
    {
        movementAllowed = true;
    }

    public void BlockMovement()
    {
        myRigidbody.velocity = Vector3.zero;
        movementAllowed = false;
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 originalScale = transform.localScale;
        originalScale.z = -1 * originalScale.z;
        transform.localScale = originalScale;
    }

    public float GetPlayerFacing()
    {
        if (facingRight)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }

    public void ThrowShuriken()
    {
        GameObject clone = Instantiate(shurikenPrefab, shurikenPosition.position, shurikenPrefab.transform.rotation);
        if (facingRight)
        {
            clone.GetComponent<Rigidbody>().AddForce(Vector3.right * throwSpeed);
        }
        else
        {
            clone.GetComponent<Rigidbody>().AddForce(Vector3.left * throwSpeed);
        }
    }

    /*
    void FlySword()
    {
        myAnimator.SetTrigger("Sword");
    }
    */
}
