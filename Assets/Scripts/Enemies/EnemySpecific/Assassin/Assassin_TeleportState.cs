using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin_TeleportState : TeleportState
{
    private Assassin enemy;
 
    public Assassin_TeleportState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_TeleportState stateData, Assassin enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
}