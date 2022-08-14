using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    private CollisionSenses CollisionSenses { get => collisionSenses ?? Core.GetCoreComponent(ref collisionSenses); }

    private CollisionSenses collisionSenses;

    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_DodgeState dodgeState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_DodgeState dodgeStateData;

    [HideInInspector]
    public Transform Caller;

    public bool isBeingCalled;
    public float callDuration = 1f;
    public Vector3 bellSquareSize = new Vector3 (1f,1f,0f);

    //  public Vector2 jumpAngle =  new Vector2(1f,1f);
    public float jumpX = 1f;
    public float jumpY =  1f;

    public override void Awake()
    {
        base.Awake();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        dodgeState = new E1_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);     
    }

    public override void Update()
    {
        base.Update();

        DebugPanel.Log("Jump Y", jumpY);
        DebugPanel.Log("Jump X", jumpX);
//        DebugPanel.Log("Jump Angle", jumpAngle);
    }

    public void Call(Transform caller)
    {
        Caller = caller;

        if (stateMachine.currentState == idleState)
        {
            idleState.Call(caller);
        }
        else if (stateMachine.currentState == moveState)
        {
            moveState.Call(caller);
        }
    }
    public void Scare(Transform scarer)
    {
        float scareXPos = scarer.position.x;
        float scareYPos = scarer.position.y;

        float nearXRange = transform.position.x + 0.5f, nearJumpX = 10f;
        float midXRange = transform.position.x + 3f, midJumpX = 20f;
        float farXRange = transform.position.x + 5f, farJumpX = 30f;
        
        float                                       nearJumpY = 20f;
        float midYRange = transform.position.x + 3f, midJumpY = 30f;
        float farYRange = transform.position.y + 6f, farJumpY = 40f;

        if (CollisionSenses.Ground)
        {
            // Set the jump Y
            if (scareYPos >= transform.position.y + 3f && scareYPos <= transform.position.y + 6f)
            {
                jumpY = midJumpY;
            }
            else if (scareYPos > transform.position.y + 6f)
            {
                jumpY = farJumpY;
            }
            else
            {
                jumpY = nearJumpY;
            }
            // Set the jump X
            if (scareXPos >= transform.position.x && scareXPos < nearXRange)
            {
                jumpX = 0f;
            }
            else if (scareXPos >= nearXRange && scareXPos < midXRange)
            {
                jumpX = nearJumpX;
            }
            else if (scareXPos >= midXRange && scareXPos < farXRange)
            {
                jumpX = midJumpX;
            }
            else
            {
                jumpX = farJumpX;
            }
            // Transition
            if (stateMachine.currentState == idleState)
            {
                idleState.Scare(scarer);
            }
            else if (stateMachine.currentState == moveState)
            {
                moveState.Scare(scarer);
            }
        }
    }
}
