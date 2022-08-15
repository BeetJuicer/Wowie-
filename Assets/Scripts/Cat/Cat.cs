using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsFish;

    private Animator anim;

    private Rigidbody2D rb;

    private float jumpStrength;
    [SerializeField] private float fallMultiplier = 2.5f;

    [SerializeField] private float strongJump = 30f;
    [SerializeField] private float averageJump = 20f;
    [SerializeField] private float weakJump = 10f;

    [SerializeField] private float movementSpeed = 10f;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool inAir;

    private bool isBeingCalled;
    private bool isBeingScared;
    private bool isSeeingFish;
    private bool isBeingTempted;
    private bool canBeTempted;

    RaycastHit2D fish;
    RaycastHit2D wallFront;
    RaycastHit2D wallBack;
    float fishDistance;
    float wallFrontDistance;
    float wallBackDistance;

    [SerializeField] private Vector2 groundCheckSize = new Vector2(1f,1f);
    [SerializeField] private Vector2 fishCheckSize = new Vector2(1f,1f);
    [SerializeField] private float wallCheckDistance = 1f;

    private Transform Caller;
    private Transform Tempter;
    private Transform Scarer;

    private int facingDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        canBeTempted = true;
    }

    void Checks()
    {
        isSeeingFish = CheckForFish();
        isGrounded = CheckIfGrounded();
        isTouchingWall = CheckIfTouchingWall();

        wallFront = Physics2D.Raycast(wallCheck.position, Vector2.right, Mathf.Infinity, whatIsGround);
        wallBack = Physics2D.Raycast(wallCheck.position, Vector2.left, Mathf.Infinity, whatIsGround);

        if (wallFront.collider != null)
        {
            wallFrontDistance = Mathf.Abs(wallFront.point.x - transform.position.x);
        }

        if (wallBack.collider != null)
        {
            wallBackDistance = Mathf.Abs(wallBack.point.x - transform.position.x);
        }

        if (fish.collider != null)
        {
            fishDistance = (fish.point.x - transform.position.x);
        }

        if (fishDistance > wallFrontDistance && (Mathf.Sign(fishDistance) != -1)|| 
            fishDistance > wallBackDistance && (Mathf.Sign(fishDistance) == -1))
        {
            canBeTempted = false;
        }
        else
        {
            canBeTempted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DebugPanel.Log("Cat can be tempted?", canBeTempted);

        Checks();

        if (!inAir && isGrounded)
        {
            if (isBeingScared && !inAir)
            {
                Jump();
                inAir = true;
            }
            else if (isBeingCalled)
            {
                GoToX(Caller, movementSpeed);

                if (Mathf.Abs(transform.position.x - Caller.position.x) < 0.5f || isTouchingWall)
                {
                    isBeingCalled = false;
                    StartCoroutine(GoodCatForSeconds(1f));
                }
            }
            else if (canBeTempted && isBeingTempted && isSeeingFish)
            {
                GoToX(Tempter, movementSpeed / 2);

                if (Mathf.Abs(transform.position.x - Tempter.position.x) < 0.3f)
                {
                    isBeingTempted = false;
                }
            }
            else
            {
                Idle();
            }
        }
        else
        {
            //in air
            if (isGrounded)
            {
                inAir = false;
                isBeingScared = false;
            }
        }       
    }

    private IEnumerator GoodCatForSeconds(float seconds)
    {
        canBeTempted = false;
        yield return new WaitForSeconds(seconds);
        canBeTempted = true;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Call(Transform caller)
    {
        isBeingCalled = true;
        Caller = caller;
    }

    public void Tempt(Transform tempter)
    {
        isBeingTempted = true;
        Tempter = tempter;
    }

    public void Scare (Transform scarer)
    {
        if (isGrounded)
        {
            Scarer = scarer;

            SetJumpStrength(scarer);
            // Handle flipping.
            isBeingScared = true;
        }
    }

    private void Jump()
    {
        // Set animator bools.
        anim.SetBool("idle", false);
        anim.SetBool("move", false);

        anim.SetBool("dodge", true);

        CheckForFlip(Scarer);
        // Handle forces
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    }

    private void GoToX(Transform target, float moveSpeed)
    {
        Vector2 rbVelocity;

        // Set animator bools.
        anim.SetBool("idle", false);
        anim.SetBool("dodge", false);

        anim.SetBool("move", true);

        CheckForFlip(target);

        rbVelocity = rb.velocity;
        rbVelocity.x = moveSpeed * facingDirection;
        rb.velocity = new Vector2(rbVelocity.x, rbVelocity.y);
    }

    public void Idle()
    {
        rb.velocity = Vector2.zero;

        // Set animator bools.
        anim.SetBool("move", false);
        anim.SetBool("dodge", false);

        anim.SetBool("idle", true);
    }

    private void SetJumpStrength(Transform scarer)
    {
        float scarerYPos = scarer.transform.position.y;

        if (scarerYPos - transform.position.y > 3f && scarerYPos - transform.position.y <= 6f) jumpStrength = averageJump;
        if (scarerYPos - transform.position.y > 0f && scarerYPos - transform.position.y <= 3f) jumpStrength = weakJump;
        if (scarerYPos - transform.position.y > 6f) jumpStrength = strongJump;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * facingDirection * wallCheckDistance);
    }

    private void Flip()
    {
        facingDirection *= -1;
        rb.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }
    public bool CheckIfGrounded()
    {
        return BoxCastDrawer.BoxCastAndDraw(groundCheck.position, groundCheckSize, 0f, Vector2.down, .02f, whatIsGround);
    }

    private bool CheckForFish()
    {
        fish = BoxCastDrawer.BoxCastAndDraw(transform.position, fishCheckSize, 0f, Vector2.right, .02f, whatIsFish);
        return BoxCastDrawer.BoxCastAndDraw(transform.position, fishCheckSize, 0f, Vector2.right, .02f, whatIsFish);
    }

    private void CheckForFlip(Transform target)
    {
        int direction = (target.transform.position.x > rb.transform.position.x + 0.5f) ? 1 : -1;
        if (facingDirection != direction) { Flip(); }
    }
}
