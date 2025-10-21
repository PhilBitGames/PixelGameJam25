using System;
using StateMachines.Unit;
using UnityEngine;

public class UnitChasingState : UnitBaseState
{
    private readonly int RunningHash = Animator.StringToHash("Running");

    public UnitChasingState(UnitStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.Play(RunningHash);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsTargetStillAlive())
        {
            stateMachine.SwitchState(new UnitAdvancingState(stateMachine));
            return;
        }

        if (IsInAttackRange())
        {
            stateMachine.SwitchState(new UnitAttackingState(stateMachine));
            return;
        }

        MoveToTarget(deltaTime);
    }


    public override void Exit()
    {
    }

    private void MoveToTarget(float deltaTime)
    {
        Vector2 directionToMove =
            (stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position).normalized;
        Move(directionToMove * stateMachine.MovementSpeed, deltaTime);
    }

    private bool IsInAttackRange()
    {
        return (stateMachine.transform.position - stateMachine.Targeter.CurrentTarget.transform.position)
               .sqrMagnitude <=
               Math.Pow(stateMachine.AttackRange, 2);
    }
}