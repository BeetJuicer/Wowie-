using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public bool CanWallClimb { get; private set; }

    public PlayerWallClimbState(PlayerScript player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        startTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerData.currentStamina -= ((Time.time - startTime) * playerData.wallClimbMultiplier);

        CanWallClimb = (playerData.currentStamina <= 0)? false : true;



        if (!isExitingState)
        {
            if (CanWallClimb)
            {
                player.SetVelocityY(playerData.wallClimbVelocity);
            }

            if (yInput != 1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if (!CanWallClimb)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }
}
