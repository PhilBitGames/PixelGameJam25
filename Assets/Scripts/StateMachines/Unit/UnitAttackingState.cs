using System.Collections;
using System.Collections.Generic;
using StateMachines.Unit;
using UnityEngine;
using Random = System.Random;

public class UnitAttackingState : UnitBaseState
{
    

    private readonly int AttackHash = Animator.StringToHash("Attack");

    public UnitAttackingState(UnitStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage + UnityEngine.Random.Range(-stateMachine.AttackDamageVariance/2, stateMachine.AttackDamageVariance/2), stateMachine.Targeter.CurrentTarget);
        
        stateMachine.Animator.Play(AttackHash);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "Attack") >= 1)
        {
            // I want whatever they're attacking to remain in focus until death
            stateMachine.SwitchState(new UnitChasingState(stateMachine));
        }
        
        
    }

    public override void Exit()
    {
        // stateMachine.Agent.ResetPath();
        // stateMachine.Agent.velocity = Vector3.zero;
    }

}
