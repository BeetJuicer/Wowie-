using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeleportState : State
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool performCloseRangeAction;

    protected Vector3 targetLocation;

    protected D_TeleportState stateData;
    public TeleportState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_TeleportState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = collisionSenses.LedgeVertical;
        isDetectingWall = collisionSenses.WallFront;

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        targetLocation = stateData.targetLocation;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void Teleport()
    {
        movement.RB.transform.position = targetLocation;
    }
}
