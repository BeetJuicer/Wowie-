using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DodgeState : DodgeState
{
    private Enemy1 enemy;
    public E1_DodgeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
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

       // Movement?.SetVelocityY(enemy.jumpY);
        Movement?.SetVelocity(enemy.jumpY, new Vector2(1f,1f), Movement.FacingDirection);
//        Movement.RB.AddForce(new Vector2(enemy.jumpX, enemy.jumpY), ForceMode2D.Impulse);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        /*
        if (isDodgeOver)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
