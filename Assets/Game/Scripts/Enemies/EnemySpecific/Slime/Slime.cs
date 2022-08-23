using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Entity
{
    public Slime_MoveState moveState { get; private set; }
    public Slime_IdleState idleState { get; private set; }
    public Slime_PlayerDetectedState playerDetectedState { get; private set; }
    public Slime_MeleeAttackState meleeAttackState { get; private set; }
    public Slime_LookForPlayerState lookForPlayerState { get; private set; }
    public Slime_DeadState deadState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
{
        base.Awake();

        moveState = new Slime_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Slime_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Slime_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new Slime_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new Slime_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new Slime_DeadState(this, stateMachine, "dead", deadStateData, this);
}
    
    private void Start()
    {
        stateMachine.Initialize(moveState);        
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}