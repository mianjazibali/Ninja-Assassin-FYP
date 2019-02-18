using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Animator myAnimator;

    //Movement Variables
    public bool movementAllowed = true;
    public float moveSpeed = 7f;

    //Player Facing Variables
    private bool facingRight;

    //Jump Variables
    public float jumpForce = 7f;
    public float extraGravity = 10f;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumpsValue = 1;
    private int extraJumps;

    //Shuriken variables 
    public GameObject shurikenPrefab;
    public Transform shurikenPosition;
    public float throwSpeed;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
        facingRight = true;
        gameObject.transform.position = GameObject.FindGameObjectWithTag("LevelManager").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
    }

    void FixedUpdate()
    {
        Collider[] groundCollisions = Physics.OverlapSphere(groundCheck.position, checkRadius, whatIsGround);
        if (groundCollisions.Length > 0)
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }

        //myAnimator.SetBool("grounded", isGrounded);

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
        else
        {
            Vector3 vel = myRigidbody.velocity;
            vel.y -= extraGravity * Time.deltaTime;
            myRigidbody.velocity = vel;
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

    public void DoJump()
    {
        if (extraJumps > 0)
        {
            myRigidbody.velocity = Vector3.up * jumpForce;
            extraJumps--;
        }
        else
        if (extraJumps == 0 && isGrounded)
        {
            myRigidbody.velocity = Vector3.up * jumpForce;
            //myAnimator.SetBool("grounded", isGrounded);
        }
    }

    void Shuriken()
    {
        myAnimator.SetTrigger("Shuriken");
    }

    public void ThrowShuriken()
    {
        float multiplier = 1;
        if(Mathf.Abs(myRigidbody.velocity.x) > 0)
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
