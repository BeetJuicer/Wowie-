using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Entity
{
    public Assassin_MoveState moveState { get; private set; }
    public Assassin_IdleState idleState { get; private set; }
    public Assassin_PlayerDetectedState playerDetectedState { get; private set; }
    public Assassin_ChargeState chargeState { get; private set; }
    public Assassin_MeleeAttackState meleeAttackState { get; private set; }
    public Assassin_DeadState deadState { get; private set; }
    public Assassin_TeleportState teleportState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_TeleportState teleportStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
{
        base.Awake();

        moveState = new Assassin_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Assassin_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Assassin_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new Assassin_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        meleeAttackState = new Assassin_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        deadState = new Assassin_DeadState(this, stateMachine, "dead", deadStateData, this);
        teleportState = new Assassin_TeleportState(this, stateMachine, "teleport", teleportStateData, this);
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