using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : State
{
    private bool isAnimationDone;
    private bool isDamaged;

    public DamageState(Entity etity, FiniteStateMachine stateMachine, string animBoolName) : base(etity, stateMachine, animBoolName)
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

    public virtual void AnimationFinished()
    {
        isDamaged = false;
        isAnimationDone = true;
    }
}
