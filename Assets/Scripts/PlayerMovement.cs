﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider capsuleCollider;
    private Animator myAnimator;
    private Rigidbody myRigidbody;

    [SerializeField]
    private float MoveSpeed;
    private bool CanMove = true;
    [SerializeField]
    private float WallCheckDistance;
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private float DoubleJumpForce;
    [SerializeField]
    private float JumpCoolDownTime;
    private float NextJumpTime;
    [SerializeField]
    private float GroundCheckDistance;
    private bool CanDoubleJump = true;
    private bool Jumped = false;
    private bool Grounded = false;
    [SerializeField]
    private float FallMultiplier;
    [SerializeField]
    private float JumpMultiplier;

    private bool MovementAllowed;
    private bool FacingRight;

    //Shuriken Variables 
    public GameObject ShurikenPrefab;
    public Transform ShurikenPosition;
    [SerializeField]
    private float ThrowSpeed = 1000f;

    //Sword Variables
    public GameObject DashOffset;
    [SerializeField]
    private float DashSpeed;
    private bool IsAttacking;
    private bool Dash;
    private bool Dashing;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        MovementAllowed = true;
        FacingRight = true;
        IsAttacking = false;
        Dash = false;
        Dashing = false;
    }

    private void FixedUpdate()
    {
        myAnimator.SetFloat("verticalSpeed", myRigidbody.velocity.y);
        if (MovementAllowed)
        {
            Move();
            if (CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
            {
                if (Grounded && Time.time > NextJumpTime)
                {
                    CanDoubleJump = true;
                    Jump(JumpForce);
                }
                else
                {
                    if(CanDoubleJump && Jumped)
                    {
                        CanDoubleJump = false;
                        Jump(DoubleJumpForce);
                    }
                }
            }
        }

        if (!Grounded)
        {
            if (myRigidbody.velocity.y < 0)
            {
                myRigidbody.velocity += Vector3.up * Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
                CanDoubleJump = false;
            }
            else
            {
                myRigidbody.velocity += Vector3.up * Physics.gravity.y * (JumpMultiplier - 1) * Time.deltaTime;
                if (myRigidbody.velocity.y > 0.2f)
                {
                    Jumped = true;
                }
            }
        }

        myAnimator.SetBool("isGrounded", Grounded);

        if (Jumped && Grounded)
        {
            Jumped = false;
            myAnimator.SetBool("isGrounded", true);
            NextJumpTime = Time.time + JumpCoolDownTime;
        }

        DashOffset.SetActive(Dashing);
        myAnimator.SetBool("swordDashing", Dashing);
        if (Dash)
        {
            myRigidbody.velocity = new Vector3(GetPlayerFacing() * DashSpeed, myRigidbody.velocity.y, 0);
        }

        if ((CrossPlatformInputManager.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Z)) && !IsAttacking)
        {
            IsAttacking = true;
            myAnimator.SetTrigger("Shuriken");
        }
        else
        if ((CrossPlatformInputManager.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.X)) && !IsAttacking)
        {
            IsAttacking = true;
            myAnimator.SetTrigger("Sword");
        }
    }

    void Move()
    {
        //float horizontalMovement = CrossPlatformInputManager.GetAxis("Horizontal");
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        myAnimator.SetFloat("moveSpeed", Mathf.Abs(horizontalMovement));

        if (horizontalMovement > 0 && !FacingRight)
        {
            FlipPlayer();
        }
        else
        if (horizontalMovement < 0 && FacingRight)
        {
            FlipPlayer();
        }

        if (CanMove)
        {
            myRigidbody.velocity = new Vector3(horizontalMovement * MoveSpeed, myRigidbody.velocity.y, 0);
        }
        //Debug.Log(myRigidbody.velocity.x);
    }

    public void SetCanMove(bool canMove)
    {
        CanMove = canMove;
    }

    void Jump(float jumpForce)
    {
        myAnimator.SetTrigger("Jump");
        myRigidbody.velocity = Vector3.up * jumpForce;
    }

    public void SetGrounded(bool grounded)
    {
        Grounded = grounded;
    }
    /*
    bool IsGrounded()
    {
        float distanceToPoints = capsuleCollider.height / 2 - capsuleCollider.radius;

        Vector3 point1 = transform.position + capsuleCollider.center + Vector3.up * distanceToPoints;
        Vector3 point2 = transform.position + capsuleCollider.center + Vector3.down * distanceToPoints;

        float radius = capsuleCollider.radius;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, Vector3.down, GroundCheckDistance);

        foreach (RaycastHit objectHit in hits)
        {
            if (objectHit.transform.tag == "Ground" || objectHit.transform.tag == "Destructible")
            {
                return true;
            }
        }

        return false;
    }
    */

    public void SetGroundCheckDistance(float distance)
    {
        GroundCheckDistance = distance;
    }

    public float GetGroundCheckDistance()
    {
        return GroundCheckDistance;
    }

    public void SwordDashing()
    {
        Attack();
        BlockMovement();
        Dashing = true;
        CanMove = false;
    }

    public void SwordNotDashing()
    {
        UnAttack();
        AllowMovement();
        Dashing = false;
        CanMove = true;
    }

    public bool isDashing()
    {
        return Dashing;
    }

    public void StartMoving()
    {
        Dash = true;
    }

    public void StopMoving()
    {
        Dash = false;
        BlockMovement();
    }

    public void AllowMovement()
    {
        MovementAllowed = true;
    }

    public void BlockMovement()
    {
        myRigidbody.velocity = Vector3.zero;
        MovementAllowed = false;
    }

    public void Attack()
    {
        IsAttacking = true;
    }

    public void UnAttack()
    {
        IsAttacking = false;
    }

    public void FlipPlayer()
    {
        FacingRight = !FacingRight;
        Vector3 originalScale = transform.localScale;
        originalScale.z = -1 * originalScale.z;
        transform.localScale = originalScale;
    }

    public float GetPlayerFacing()
    {
        if (FacingRight)
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
        float tempThrowSpeed = ThrowSpeed;
        GameObject clone = Instantiate(ShurikenPrefab, ShurikenPosition.position, ShurikenPrefab.transform.rotation);
        if (Mathf.Abs(myRigidbody.velocity.x) > 0) tempThrowSpeed = ThrowSpeed * 1.5f;
        if (FacingRight)
        {
            
            clone.GetComponent<Rigidbody>().AddForce(Vector3.right * tempThrowSpeed);
        }
        else
        {
            clone.GetComponent<Rigidbody>().AddForce(Vector3.left * tempThrowSpeed);
        }
    }
}
