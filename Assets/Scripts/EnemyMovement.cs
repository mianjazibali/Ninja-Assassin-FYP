using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float walkSpeed = 10f;
    public float runSpeed = 10f;
    private float moveSpeed;
    public float stayDuration = 3f;
    public float attackDuration = 3f;
    public float deathDuration = 5f;
    public List<Transform> objectToFade;
    public bool canSee = false;
    public Transform seePointA;
    public Transform seePointB;
    public float sightOffset = 10f;

    //Shuriken Variables 
    public GameObject shurikenPrefab;
    public Transform shurikenPosition;
    [SerializeField]
    private float throwSpeed = 1000f;

    private Rigidbody myRigidbody;
    private Animator myAnimator;

    private bool facingRight = true;
    private float moveInput = 1;
    private bool isLooking = false;
    private bool isAttacking = false;

    //Temporary point to store patrolling points
    private Transform tempPointA;
    private Transform tempPointB;

    public enum Difficulty {Easy, Normal, Hard, Challenging};
    public Difficulty difficulty = Difficulty.Easy;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        myAnimator.SetInteger("Difficulty", (int)difficulty);
        moveSpeed = walkSpeed;
        tempPointA = pointA;
        tempPointB = pointB;
    }

    private void Update()
    {
        if (canSee)
        {
            RaycastHit hit;
            Vector3 sightDirection;
            float sightDistance;
            if (facingRight)
            {
                sightDirection = Vector3.right;
                sightDistance = (pointB.position.x - transform.position.x) + sightOffset;
            }
            else
            {
                sightDirection = Vector3.left;
                sightDistance = (transform.position.x - pointA.position.x) + sightOffset;
            }
            if(Physics.Raycast(transform.position, sightDirection, out hit, sightDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("PlayerInRange" + hit.transform.name);
                    if (difficulty == Difficulty.Normal || difficulty == Difficulty.Easy)
                    {
                        moveSpeed = runSpeed;
                        if (facingRight)
                        {
                            pointB = seePointB;
                        }
                        else
                        {
                            pointA = seePointA;
                        }
                    }
                    else
                    if (difficulty == Difficulty.Hard)
                    {
                        InstantAttack();
                    }
                }
                else
                {
                    if (difficulty == Difficulty.Normal || difficulty == Difficulty.Easy)
                    {
                        moveSpeed = walkSpeed;
                    }
                }
            }
            else
            {
                if (difficulty == Difficulty.Normal || difficulty == Difficulty.Easy)
                {
                    moveSpeed = walkSpeed;
                }
            }
        }
        myAnimator.SetBool("isAttacking", isAttacking);
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            DelayedDeath();
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
        if (facingRight)
        {
            pointB = tempPointB;
        }
        else
        {
            pointA = tempPointA;
        }
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
        isAttacking = false;
        isLooking = false;
    }

    public void InstantAttack()
    {
        if (!isAttacking)
        {
            StopAllCoroutines();
            StartCoroutine(DelayedAttack());
        }    
    }

    IEnumerator DelayedAttack()
    {
        isAttacking = true;
        float temp = moveInput;
        moveInput = 0f;
        myRigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        isLooking = false;
        if (temp == 0 && facingRight)
        {
            moveInput = 1f;
        }
        else
        if(temp == 0 && !facingRight)
        {
            moveInput = -1f;
        }
        else
        {
            moveInput = temp;
        }
    }

    public void DelayedDeath()
    {
        StopAllCoroutines();
        isAttacking = true;
        isLooking = true;
        moveInput = 0f;
        myRigidbody.velocity = Vector3.zero;
        myAnimator.SetTrigger("Death");
    }

    public void FadeOut()
    {
        foreach (Transform t in objectToFade)
        {
            foreach (Material m in t.GetComponent<Renderer>().materials)
            {
                m.SetFloat("_Mode", 2);
                m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                m.SetInt("_ZWrite", 0);
                m.DisableKeyword("_ALPHATEST_ON");
                m.EnableKeyword("_ALPHABLEND_ON");
                m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                m.renderQueue = 3000;
            }
        }
        iTween.FadeTo(gameObject, 0f, 2f);
        StartCoroutine(DestroyAfterFadeOut());
    }

    IEnumerator DestroyAfterFadeOut()
    {
        yield return new WaitForSeconds(2f);
        Destroy(transform.parent.gameObject);
    }

    public void ThrowShuriken()
    {
        float tempThrowSpeed = throwSpeed;
        if (facingRight)
        {
            shurikenPrefab.transform.localScale = new Vector3(10, 10, 10);
        }
        else
        {
            shurikenPrefab.transform.localScale = new Vector3(-10, 10, 10);
        }
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
