using StateMachines.Unit;
using UnityEngine;

public class UnitAttackingState : UnitBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");

    public UnitAttackingState(UnitStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        var damageVariance =
            Random.Range(-stateMachine.AttackDamageVariance / 2, stateMachine.AttackDamageVariance / 2);
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage + damageVariance, stateMachine.Targeter.CurrentTarget);
        stateMachine.Animator.Play(AttackHash);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "Attack") >= 1)
            stateMachine.SwitchState(new UnitChasingState(stateMachine));
    }

    public override void Exit()
    {
    }
}