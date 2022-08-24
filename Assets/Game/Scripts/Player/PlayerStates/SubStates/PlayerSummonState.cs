using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSummonState : PlayerAbilityState
{
    private Vector3 summonPosition;

    private bool isHolding;

    private float holdStartTime;

    public PlayerSummonState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        summonPosition = player.transform.position + (playerData.summonOffset * Movement.FacingDirection);

        player.Anim.SetFloat("summonCastDuration", 1);

        Movement?.SetVelocityZero();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isHolding)
        {
            player.Anim.SetFloat("summonCastDuration", (holdStartTime + playerData.summonCastDuration) - Time.time);
        }

        Movement?.SetVelocityZero();
    }

    #region Animation Triggers

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHolding = true;
        holdStartTime = Time.time;
    }

    public override void AnimationSummonTrigger()
    {
        GameObject.Instantiate(playerData.skeletonSoldier, summonPosition, Quaternion.identity);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    #endregion
}