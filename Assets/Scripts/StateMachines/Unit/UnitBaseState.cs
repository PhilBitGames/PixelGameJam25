using System;
using System.Collections;
using System.Collections.Generic;
using StateMachines.Unit;
using UnityEngine;

public abstract class UnitBaseState : State
{
    protected UnitStateMachine stateMachine;

    public UnitBaseState(UnitStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsInChaseRange()
    {
        return (stateMachine.transform.position - stateMachine.Targeter.CurrentTarget.transform.position).sqrMagnitude <=
               Math.Pow(stateMachine.ChasingRange, 2);
    }
    
    protected bool IsTargetStillAlive()
    {
        return stateMachine.Targeter.CurrentTarget != null && !stateMachine.Targeter.CurrentTarget.GetComponent<Health>().IsDead;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        // stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        stateMachine.transform.Translate(motion * deltaTime);
    }
}
