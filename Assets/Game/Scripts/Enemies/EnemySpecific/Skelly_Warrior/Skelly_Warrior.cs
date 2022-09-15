using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_Warrior : Entity
{
    public Skelly_Warrior_MoveState moveState { get; private set; }
    public Skelly_Warrior_IdleState idleState { get; private set; }
    public Skelly_Warrior_PlayerDetectedState playerDetectedState { get; private set; }
    public Skelly_Warrior_ChargeState chargeState { get; private set; }
    public Skelly_Warrior_MeleeAttackState meleeAttackState { get; private set; }
    public Skelly_Warrior_LookForPlayerState lookForPlayerState { get; private set; }
    public Skelly_Warrior_DeadState deadState { get; private set; }

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
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
{
        base.Awake();

        moveState = new Skelly_Warrior_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Skelly_Warrior_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Skelly_Warrior_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new Skelly_Warrior_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        meleeAttackState = new Skelly_Warrior_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new Skelly_Warrior_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new Skelly_Warrior_DeadState(this, stateMachine, "dead", deadStateData, this);
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