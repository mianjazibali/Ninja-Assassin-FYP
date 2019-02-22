using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Animator myAnimator;

    //Running Variables
    private bool movementAllowed;
    [SerializeField]
    private readonly float moveSpeed = 15f;
    private bool facingRight;

    //Jumping Variables
    private readonly float jumpVelocity = 15f;
    private readonly float fallMultiplier = 3f;
    private readonly float lowJumpMultiplier = 1.5f;
    public bool isGrounded;
    public Transform rayCastSource;
    private readonly float rayCastLength = 4f;
    public LayerMask groundLayer;
    private float colliderHeight;
    public float heightFactor = 0f; //Move RayCast Source Position Little Bit Up By Factor

    //Shuriken variables 
    public GameObject shurikenPrefab;
    public Transform shurikenPosition;
    [SerializeField]
    [Range(500, 1500)]
    private float throwSpeed = 1000f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        movementAllowed = true;
        facingRight = true;
        isGrounded = true;
        colliderHeight = GetComponent<CapsuleCollider>().height;
    }

    private void FixedUpdate()
    {
        //Movement
        float moveInput = 0f;
        moveInput = CrossPlatformInputManager.GetAxis("Horizontal");
        if (moveInput < 0 && facingRight && movementAllowed)
        {
            FlipPlayer();
        }
        else if (moveInput > 0 && !facingRight && movementAllowed)
        {
            FlipPlayer();
        }
        if (movementAllowed)
        {
            myRigidbody.velocity = new Vector3(moveInput * moveSpeed, myRigidbody.velocity.y, 0f);
        }
        if (isGrounded)
        {
            myAnimator.SetFloat("moveSpeed", Mathf.Abs(moveInput));
        }

        //Jumping
        if(myRigidbody.velocity.y < -0.1)
        {
            myAnimator.SetBool("isFalling", true);
        }
        else
        {
            myAnimator.SetBool("isFalling", false);
        }
        //Debug.Log(Physics.Raycast(transform.position + (Vector3.right * 0.55f), Vector3.down, rayCastLength));
        //Debug.Log(Physics.Raycast(transform.position + (Vector3.left * 0.5f), Vector3.down, rayCastLength));
        Debug.DrawRay(transform.position + (Vector3.left * 0.55f) + (Vector3.up * heightFactor), Vector3.down, Color.red, 0.1f, true);
        Debug.DrawRay(transform.position + (Vector3.right * 0.5f) + (Vector3.up * heightFactor), Vector3.down, Color.red, 0.1f, true);
        if (Physics.Raycast(transform.position + (Vector3.right * 0.55f) + (Vector3.up * heightFactor), Vector3.down, rayCastLength) || Physics.Raycast(transform.position + (Vector3.left * 0.5f) + (Vector3.up * heightFactor), Vector3.down, rayCastLength))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        myAnimator.SetBool("isGrounded", isGrounded);
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
        {
            myRigidbody.velocity = Vector3.up * jumpVelocity;
            myAnimator.SetTrigger("Jump");
        }
        if(myRigidbody.velocity.y < 0)
        {
            myRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else
        if(myRigidbody.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump")){
            myRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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

    public void ShrinkCollider()
    {
        heightFactor = 0.5f;
        GetComponent<CapsuleCollider>().height = colliderHeight/1.36f;
    }

    public void RevertCollider()
    {
        heightFactor = 0f;
        GetComponent<CapsuleCollider>().height = colliderHeight;
    }

    public void ThrowShuriken()
    {
        float multiplier = 1;
        if (Mathf.Abs(myRigidbody.velocity.x) > 0)
        {
            multiplier = 1.33f;
        }
        GameObject clone = Instantiate(shurikenPrefab, shurikenPosition.position, shurikenPrefab.transform.rotation);
        if (facingRight)
        {
            clone.GetComponent<Rigidbody>().AddForce(Vector3.right * throwSpeed * multiplier);
        }
        else
        {
            clone.GetComponent<Rigidbody>().AddForce(Vector3.left * throwSpeed * multiplier);
        }
    }

    void FlySword()
    {
        myAnimator.SetTrigger("Sword");
    }
}
