﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Animator myAnimator;

    //Running Variables
    bool movementAllowed;
    public float runSpeed = 15f;
    public float autoMovingSpeed = 10f;
    bool facingRight;

    //Wall Check Variables
    public float wallCheckDistance;

    //Jumping Variables
    bool grounded = false;
    Collider[] groundCollisions;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public Transform groundChecker;
    public float jumpForce;
    public float fallMultiplier;
    public float jumpMultiplier;
    private bool jumped;
    public float jumpDelayTime;
    private float nextJump;
    private bool canDoubleJump;

    //Shuriken Variables 
    public GameObject shurikenPrefab;
    public Transform shurikenPosition;
    [SerializeField]
    private float throwSpeed = 1000f;

    //Sword Variables
    private bool isAttacking;
    private bool startMoving;
    private bool swordDashing;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        movementAllowed = true;
        facingRight = true;
        jumped = false;
        canDoubleJump = true;
        isAttacking = false;
        startMoving = false;
        swordDashing = false;
    }

    private void Awake()
    {
        nextJump = 0f;
    }

    private void FixedUpdate()
    {
        myAnimator.SetFloat("verticalSpeed", myRigidbody.velocity.y);
        if (CrossPlatformInputManager.GetButtonDown("Jump") && movementAllowed)
        {
            if(grounded && nextJump < Time.time)
            {
                canDoubleJump = true;
                myAnimator.SetTrigger("Jump");
                myRigidbody.velocity = Vector3.up * jumpForce;
            }
            else
            {
                if (canDoubleJump && jumped)
                {
                    canDoubleJump = false;
                    myAnimator.SetTrigger("Jump");
                    myRigidbody.velocity = Vector3.up * jumpForce * 1.5f;
                }
            }
        }
        if(myRigidbody.velocity.y < 0)
        {
            myRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            canDoubleJump = false;
        }
        else
        {
            myRigidbody.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
            if(myRigidbody.velocity.y > 1)
            {
                jumped = true;
            }
        }

        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 1f);
        groundCollisions = Physics.OverlapSphere(groundChecker.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;
        myAnimator.SetBool("isGrounded", grounded);

        if(jumped && grounded)
        {
            nextJump = Time.time + jumpDelayTime;
            jumped = false;
        }

        myAnimator.SetBool("swordDashing", swordDashing);
        if (startMoving)
        {
            myRigidbody.velocity = new Vector3(GetPlayerFacing() * autoMovingSpeed, myRigidbody.velocity.y, 0);
        }

        if (movementAllowed)
        {
            float move = CrossPlatformInputManager.GetAxis("Horizontal");
            if (move > 0 && !facingRight) FlipPlayer();
            else if (move < 0 && facingRight) FlipPlayer();
            //Debug.DrawRay(groundCheck.position + Vector3.up * 1f, Vector3.right * GetPlayerFacing(), Color.red, 1f);
            if (Physics.Raycast(groundChecker.position + Vector3.up * 1f, Vector3.right * GetPlayerFacing(), wallCheckDistance, groundLayer))
            {
                move = 0;
            }
            myAnimator.SetFloat("moveSpeed", Mathf.Abs(move));
            myRigidbody.velocity = new Vector3(move * runSpeed, myRigidbody.velocity.y, 0);
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            myAnimator.SetTrigger("Shuriken");
        }
        else
        if (CrossPlatformInputManager.GetButtonDown("Fire2") && !isAttacking)
        {
            isAttacking = true;
            myAnimator.SetTrigger("Sword");
        }
        else
        if (CrossPlatformInputManager.GetButtonDown("Fire3"))
        {
            //myAnimator.SetTrigger("Shuriken");
        }
    }

    public void SwordDashing()
    {
        Attack();
        BlockMovement();
        swordDashing = true;
    }

    public void SwordNotDashing()
    {
        UnAttack();
        AllowMovement();
        swordDashing = false;
    }

    public void StartMoving()
    {
        startMoving = true;
    }

    public void StopMoving()
    {
        startMoving = false;
        BlockMovement();
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

    public void Attack()
    {
        isAttacking = true;
    }

    public void UnAttack()
    {
        isAttacking = false;
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
        float tempThrowSpeed = throwSpeed;
        GameObject clone = Instantiate(shurikenPrefab, shurikenPosition.position, shurikenPrefab.transform.rotation);
        if (Mathf.Abs(myRigidbody.velocity.x) > 0) tempThrowSpeed = throwSpeed * 1.5f;
        if (facingRight)
        {
            
            clone.GetComponent<Rigidbody>().AddForce(Vector3.right * tempThrowSpeed);
        }
        else
        {
            clone.GetComponent<Rigidbody>().AddForce(Vector3.left * tempThrowSpeed);
        }
    }
}
