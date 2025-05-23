using System.Collections;
using System.Collections.Generic;
using StateMachines.Unit;
using Units;
using UnityEngine;

public class UndeadMovingState : UnitBaseState
{
    private readonly int EntryHash = Animator.StringToHash("Entry");

    public MoveTarget moveTarget;

    public UndeadMovingState(UnitStateMachine stateMachine, MoveTarget moveTarget) : base(stateMachine)
    {
        this.moveTarget = moveTarget;
    }

    public override void Enter()
    {
        stateMachine.Animator.Play(EntryHash);
    }

    public override void Tick(float deltaTime)
    {
        MoveToTarget(deltaTime);
    }

    public override void Exit()
    {
        moveTarget.DisregardUnit(stateMachine);
    }
    
    private void MoveToTarget(float deltaTime)
    {
        Vector2 directionToMove = (moveTarget.transform.position - stateMachine.transform.position).normalized;
        Move(directionToMove, deltaTime);
    }

}
