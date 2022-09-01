using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DamageState : DamageState
{
    public E1_DamageState(Entity etity, FiniteStateMachine stateMachine, string animBoolName) : base(etity, stateMachine, animBoolName)
    {
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
    public override void AnimationFinished()
    {
        base.AnimationFinished();
    }
}
