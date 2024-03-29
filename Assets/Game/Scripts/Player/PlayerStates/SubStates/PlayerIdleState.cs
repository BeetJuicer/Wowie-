﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState {
	public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	private bool dashInput;
	private bool dodgeInput;

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		Movement?.SetVelocityX(0f);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		dashInput = player.InputHandler.DashInput;
		dodgeInput = player.InputHandler.DodgeInput;

		if (!isExitingState)
		{
			if (dodgeInput && player.DodgeState.CheckIfCanDodge())
			{
				stateMachine.ChangeState(player.DodgeState);
			}
			else if (player.InputHandler.FireInput && player.SummonState.CanSummon())
            {
				stateMachine.ChangeState(player.SummonState);
            }
			/*
			else if (dashInput && player.DashState.CheckIfCanDash())
			{
				stateMachine.ChangeState(player.DashState);
			}*/
			else if (xInput != 0)
			{
				stateMachine.ChangeState(player.MoveState);
			}
		/* else if (yInput == -1) 
		 * {
			stateMachine.ChangeState(player.CrouchIdleState);
			}*/
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
