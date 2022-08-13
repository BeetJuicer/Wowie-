﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State {
	private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
	private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

	private Movement movement;
	private CollisionSenses collisionSenses;

	protected D_MoveState stateData;

	protected bool isDetectingWall;
	protected bool isDetectingLedge;
	protected bool isPlayerInMinAgroRange;

	protected bool callFlip;
	protected bool isScared;

	protected int amountOfStops;
	//-Dodge
	protected float strength;
	protected Vector2 angle;

	public MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(etity, stateMachine, animBoolName) {
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();

		isDetectingLedge = CollisionSenses.LedgeVertical;
		isDetectingWall = CollisionSenses.WallFront;
		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
	}

	public override void Enter() {
		base.Enter();
		Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);
	}

	public override void Exit() {
		base.Exit();
	}

	public void Call(Transform caller)
	{
		int direction = (caller.transform.position.x > movement.RB.transform.position.x) ? 1 : -1;
		if (direction != movement.FacingDirection)
		{
			movement.Flip();
		}
	}

	public void Scare(Transform scarer)
    {
		int direction = (scarer.transform.position.x > movement.RB.transform.position.x) ? 1 : -1;
		if (direction != movement.FacingDirection)
		{
			movement.Flip();
		}

		strength = (scarer.transform.position.y - movement.RB.transform.position.y >= 1f) ? 40 : 20;
		angle = new Vector2(2f, 1f);
		isScared = true;
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
