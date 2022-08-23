using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerAbilityState
{
    private int amountOfDodgesLeft;

    private float lastDodgeTime;
    private float origGravity;
    public PlayerDodgeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfDodgesLeft = playerData.amountOfDodges;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseDodgeInput();

        // Store the original gravity in a older and turn off gravity
        origGravity = player.RB.gravityScale;
        player.TurnOffGravity();

        Movement?.SetVelocityY(0f);
        Movement?.SetVelocityX(playerData.dodgeVelocity * Movement.FacingDirection);

        startTime = Time.time;

        amountOfDodgesLeft--;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.TurnOffGravity();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        DebugPanel.Log("Player Gravity", player.RB.gravityScale);

        if (!isExitingState)
        {
            Movement?.SetVelocityY(0f);
            Movement?.SetVelocityX(playerData.dodgeVelocity * Movement.FacingDirection);
            // Set invincible

            if (Time.time > startTime + playerData.dodgeDuration)
            {
                player.RB.gravityScale = origGravity;
                isAbilityDone = true;
                lastDodgeTime = Time.time;
            }
        }
    }

    public bool CanDodge()
    {
        if (Time.time >= lastDodgeTime + playerData.dashCooldown) 
        {
            ResetAmountOfDodgesLeft(); 
        }

        if (amountOfDodgesLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfCanDodge()
    {
        return CanDodge() && Time.time >= lastDodgeTime + playerData.dodgeCooldown;
    }

    public void ResetAmountOfDodgesLeft() => amountOfDodgesLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfDodgesLeft() => amountOfDodgesLeft--;
}
