using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public bool CanWallSlide { get; private set; }

    public PlayerWallSlideState(PlayerScript player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        
        if (!isExitingState)
        {
            player.SetVelocityY(-playerData.wallSlideVelocity);
           
            if (grabInput && yInput == 0)// controller not == 0
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
