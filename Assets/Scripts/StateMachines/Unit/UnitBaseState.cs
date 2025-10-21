using StateMachines;
using StateMachines.Unit;
using UnityEngine;

public abstract class UnitBaseState : State
{
    protected UnitStateMachine stateMachine;

    protected UnitBaseState(UnitStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsTargetStillAlive()
    {
        return stateMachine.Targeter.CurrentTarget != null &&
               !stateMachine.Targeter.CurrentTarget.GetComponent<Health>().IsDead;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.transform.Translate(motion * deltaTime);
    }
}