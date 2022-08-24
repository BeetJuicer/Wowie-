using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerAbilityState
{
    private int amountOfDodgesLeft;

    private float lastDodgeTime;
    private float origGravity;

    private Vector2 lastAIPos;

    public PlayerDodgeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfDodgesLeft = playerData.amountOfDodges;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseDodgeInput();
        Combat?.SetInvincible(true);

        // Turn off gravity and set velocity to 0 to mae sure that the player doesn't fall while dodging.
        origGravity = player.RB.gravityScale;
        player.TurnOffGravity();
        Movement?.SetVelocityY(0f);

        player.RB.drag = playerData.drag;
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
        CheckIfCanDodge();

        if (!isExitingState)
        {
            CheckIfShouldPlaceAfterImage();
            Movement?.SetVelocityX(playerData.dodgeVelocity * Movement.FacingDirection);

            if (Time.time > startTime + playerData.dodgeDuration)
            {
                player.RB.drag = 0f;
                player.RB.gravityScale = origGravity;
                isAbilityDone = true;
                Combat.SetInvincible(false);
                lastDodgeTime = Time.time;
            }
        }
    }

    public bool CheckIfCanDodge()
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

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAIPos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;
    }

    public void ResetAmountOfDodgesLeft() => amountOfDodgesLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfDodgesLeft() => amountOfDodgesLeft--;
}
