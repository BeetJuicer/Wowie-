using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool isLedgeTooLow;
    private bool isWallTallEnough;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool grabInput;
    private bool dashInput;

    protected bool isTouchingCeiling;
    public PlayerGroundedState(PlayerScript player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isLedgeTooLow = player.CheckLedgeHeight();
        isWallTallEnough = player.CheckWallHeight();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingCeiling = player.CheckForCeiling();
        isTouchingLedge = player.CheckIfTouchingLedge();
        player.DashState.ResetCanDash();
    }

    public override void Enter()
    {
        base.Enter();
        playerData.currentStamina = playerData.maxStamina;
        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        JumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        if (JumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime(); 
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && grabInput && isTouchingLedge && isWallTallEnough)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
