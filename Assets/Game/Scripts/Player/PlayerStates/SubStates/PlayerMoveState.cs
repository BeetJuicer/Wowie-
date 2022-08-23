using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState {
	public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	private bool dashInput;
	private bool dodgeInput;

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		Movement?.CheckIfShouldFlip(xInput);

		Movement?.SetVelocityX(playerData.movementVelocity * xInput);

		dashInput = player.InputHandler.DashInput;
		dodgeInput = player.InputHandler.DodgeInput;

		if (!isExitingState) {

			if (dodgeInput && player.DodgeState.CanDodge())
			{
				stateMachine.ChangeState(player.DodgeState);
			}
			else if (dashInput && player.DashState.CheckIfCanDash())
			{
				stateMachine.ChangeState(player.DashState);
			}
			else if (xInput == 0) {
				stateMachine.ChangeState(player.IdleState);
			}/* else if (yInput == -1) {
				stateMachine.ChangeState(player.CrouchMoveState);
			}*/
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
