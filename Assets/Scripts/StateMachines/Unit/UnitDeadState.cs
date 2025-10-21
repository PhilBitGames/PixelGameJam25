using Combat;
using StateMachines.Unit;
using UnityEngine;

public class UnitDeadState : UnitBaseState
{
    public UnitDeadState(UnitStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if (stateMachine.Faction == Faction.Enemy)
        {
            var deadGameObject = Object.Instantiate(stateMachine.DeadPrefab, stateMachine.transform.position,
                Quaternion.identity);
            deadGameObject.GetComponentInChildren<SpriteRenderer>().flipX =
                stateMachine.Visual.GetComponent<SpriteRenderer>().flipX;
            deadGameObject.GetComponent<SelectableUnit>().UnitType = stateMachine.UnitDefinition.id;
        }

        stateMachine.DestroyThis();
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}