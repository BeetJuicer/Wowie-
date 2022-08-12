using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    float runDuration;
    public PlayerMoveState(PlayerScript player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.InvokeRepeating("PlayMoveAudio", 0f, 0.2f);
        startTime = Time.time;
    }

    public override void Exit()
    {
        player.CancelInvoke();
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);
            
        player.SetVelocityX(playerData.movementVelocity * xInput);

        if (!isExitingState)
            {
                if (xInput == 0)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else if (player.InputHandler.DashInput && player.DashState.CheckIfCanDash())
                {
                    stateMachine.ChangeState(player.DashState);
                }
            }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
