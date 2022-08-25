using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSummonState : PlayerAbilityState
{
    private Vector3 summonPosition;

    private bool isHolding;
    private bool fireInputStop;

    private float holdStartTime;

    public PlayerSummonState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseFireInput();

        summonPosition = player.transform.position + (playerData.summonOffset * Movement.FacingDirection);

        player.Anim.SetFloat("summonCastDuration", 1);

        Movement?.SetVelocityZero();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityZero();



        if (!isExitingState)
        {
            if (isHolding)
            {
                player.Anim.SetFloat("summonCastDuration", (holdStartTime + playerData.summonCastDuration) - Time.time);
                fireInputStop = player.InputHandler.FireInputStop;

                if (fireInputStop)
                {
                    player.Anim.SetFloat("summonCastDuration", 0);
                    isAbilityDone = true;
                    isHolding = false;
                }
                else if ((holdStartTime + playerData.summonCastDuration) - Time.time <= 0)
                {
                    GameObject.Instantiate(playerData.skeletonSoldier, summonPosition, Quaternion.identity);
                    isAbilityDone = true;
                    isHolding = false;
                }
            }
        }

    }

    #region Animation Triggers

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHolding = true;
        holdStartTime = Time.time;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    #endregion
}