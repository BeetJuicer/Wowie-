using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }

    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }

    public PlayerDashState DashState { get; private set; }/*
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }*/

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public AudioManager AudioManager { get; private set; }
    //------------------------//
    public PlayerHandler playerHandler { get; private set; }
    public GameObject trails;

    [SerializeField]
    private GameObject landParticle;
    [SerializeField]
    private GameObject jumpParticle;
    [SerializeField]
    private GameObject dashParticle;
    [SerializeField]
    private ParticleSystem dustParticle;
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ceilingCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform wallHeightCheck;

    private LayerMask candyLayer;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public float playerGravity { get; private set; }

    private Vector2 workspace;
    [HideInInspector]
    public string abilityUsed;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");/*
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");*/
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MovementCollider = GetComponent<BoxCollider2D>();

        AudioManager = GameObject.FindWithTag("AudioManager").gameObject.GetComponent<AudioManager>();
        playerHandler = GetComponent<PlayerHandler>();

        candyLayer = (playerHandler.cake) ? playerData.whatIsCakeGround : playerData.whatIsIceCreamGround;

        FacingDirection = 1;
        playerGravity = RB.gravityScale;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        UpdateLog();

        if (PlayerInputHandler.GetInstance().GetInteractPressed())
        {
                GameManager.isCake = !GameManager.isCake;
        }

        if (gameObject.GetComponent<PlayerHandler>().cake == false)
        {
            if (GameManager.isCake == true)
            {
                CurrentVelocity = RB.velocity;
                StateMachine.ChangeState(IdleState);
                return;
            }
        }
        else
        {
            if (GameManager.isCake == false)
            {
                CurrentVelocity = RB.velocity;
                StateMachine.ChangeState(IdleState);
                return;
            }
        }

        if (PauseMenu.gamePaused)
        {
            return;
        }
        if (DialogueManager.GetInstance().dialogueIsPlaying || CutsceneTracker.GetInstance().cutsceneIsPlaying)
        {
            SetVelocityY(CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
            StateMachine.ChangeState(IdleState);
            return;
        }
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        if (!playerHandler.isDead)
        {
            workspace.Set(velocity, CurrentVelocity.y);
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }

    public void SetVelocityY(float velocity)
    {
        if (!playerHandler.isDead)
        {
            workspace.Set(CurrentVelocity.x, velocity);
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        if (!playerHandler.isDead)
        {
            workspace = direction * velocity;
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        if (!playerHandler.isDead)
        {
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }
    #endregion

    #region Check Functions


    public bool CheckIfGrounded()
    {
        //return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
        return BoxCastDrawer.BoxCastAndDraw(groundCheck.position, playerData.groundCheckBoxSize, 0f, Vector2.down, .02f, playerData.whatIsGround) ||
               BoxCastDrawer.BoxCastAndDraw(groundCheck.position, playerData.groundCheckBoxSize, 0f, Vector2.down, .02f, candyLayer);
        
    }

    public bool CheckLedgeHeight()
    {
        return BoxCastDrawer.BoxCastAndDraw(groundCheck.position, playerData.ledgeHeightBoxSize, 0f, Vector2.down, playerData.ledgeHeightBoxSize.y, playerData.whatIsGround);
    }

    public bool CheckLedgeDepth() //not working yet.
    {
        return BoxCastDrawer.BoxCastAndDraw(ceilingCheck.position, playerData.ledgeHeightBoxSize, 0f, Vector2.right, playerData.ledgeHeightBoxSize.y, playerData.whatIsGround);
    }

    public bool CheckWallHeight()
    {
        return BoxCastDrawer.BoxCastAndDraw(wallHeightCheck.position, playerData.wallHeightBoxSize, 0f, Vector2.right * FacingDirection, playerData.wallHeightBoxSize.x, playerData.whatIsGround);
    }

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround, 0, 0);
    }
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround, 0, 0);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();


    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
        float yDist = yHit.distance;

        workspace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist);
        return workspace;
    }

    private void Flip()
    {
        CreateDust();
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public void SpawnLandFX()
    {
        Instantiate(landParticle, transform.position, landParticle.transform.rotation);
    }

    public void SpawnJumpFX()
    {
        Instantiate(jumpParticle, (transform.position) - new Vector3(0f, .3f, 0f), jumpParticle.transform.rotation);
    }

    public void SpawnWallJumpFX()
    {
        Instantiate(jumpParticle, transform.position, Quaternion.Euler(0f, 0f, 90f * FacingDirection));
    }
    public void SpawnDashFX(float angle)
    {
        Instantiate(dashParticle, transform.position, Quaternion.Euler(0f, 0f, angle));
    }
    private void PlayMoveAudio()
    {
        AudioManager.Play("Move");
    }

    private void CreateDust()
    {
        dustParticle.Play();
    }

    private void OnDrawGizmos()
    {
        //Debug.Log("OnDrawGizmos is being called.");
        if (wallCheck == null || groundCheck == null)
        {
            Debug.Log("Wallcheck or Groundcheck is null.");
            return;
        }
        //---Wallcheck.
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDirection * playerData.wallCheckDistance));
        //---Ceiling check.
     //   Gizmos.DrawWireSphere(ceilingCheck.position, playerData.groundCheckRadius);

        //---LedgeHeight box.
//        Debug.DrawRay(groundCheck.position + new Vector3(playerData.ledgeHeightBoxSize.x, 0), Vector2.down * (playerData.ledgeHeightBoxSize.y + 1f - 0.5f), Color.blue);
//        Debug.DrawRay(groundCheck.position - new Vector3(playerData.ledgeHeightBoxSize.x, 0), Vector2.down * (playerData.ledgeHeightBoxSize.y + 1f - 0.5f), Color.blue);
        //Debug.DrawRay(groundCheck.position - new Vector3(playerData.ledgeHeightBoxSize.x, playerData.ledgeHeightBoxSize.y), Vector2.right * (playerData.ledgeHeightBoxSize.y + 1f), Color.blue);
        //---WallHeight Box.
//        Debug.DrawRay(wallHeightCheck.position + new Vector3(0, playerData.wallHeightBoxSize.y - 0.5f), (Vector2.right * FacingDirection) * (playerData.wallHeightBoxSize.x), Color.blue);
//        Debug.DrawRay(wallHeightCheck.position - new Vector3(0, playerData.wallHeightBoxSize.y - 0.5f), (Vector2.right * FacingDirection) * (playerData.wallHeightBoxSize.x), Color.blue);
        //---Ground check circle.
       // Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
    }

    private void UpdateLog()
    {
        // Debug.Log(StateMachine.CurrentState);
        // Debug.Log("Stamina: " + playerData.currentStamina);
        // Debug.Log("Is ledge high enough?: " + CheckLedgeHeight());
        // Debug.Log("isTouchingWall && !isTouchingLedge && !isGrounded && isLedgeHighEnough " + CheckIfTouchingWall() + CheckIfTouchingLedge() + CheckIfGrounded() + CheckLedgeHeight());
        // Debug.Log("Wall height check touching?: " + CheckWallHeight());
    }

    public static void LogValue()
    {
        //
    }
        #endregion
}

