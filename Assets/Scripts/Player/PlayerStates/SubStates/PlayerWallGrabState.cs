using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;

    public bool CanWallGrab { get; private set; }
    public PlayerWallGrabState(PlayerScript player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        holdPosition = player.transform.position;
        startTime = Time.time;
        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerData.currentStamina -= ( (Time.time - startTime) * playerData.wallGrabMultiplier );

        CanWallGrab = (playerData.currentStamina <= 0) ? false : true;

        if (!isExitingState)
        {
            if (CanWallGrab)
            {
                HoldPosition();
            }
            if(!CanWallGrab)
            {
                stateMachine.ChangeState(player.InAirState);
            }
            else if (yInput > 0 && playerData.currentStamina > 0f)
            {
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
    }

    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        player.SetVelocityX(0);
        player.SetVelocityY(0);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
