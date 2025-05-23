using System.Collections;
using System.Collections.Generic;
using Combat;
using StateMachines.Unit;
using UnityEngine;

public class UnitDeadState : UnitBaseState
{
    
    private readonly int DieHash = Animator.StringToHash("Die");

    // Start is called before the first frame update
    public UnitDeadState(UnitStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    { 
        //stateMachine.Animator.Play(DieHash);

        if (stateMachine.Faction == Faction.Enemy)
        {
            var dead = GameObject.Instantiate(stateMachine.DeadPrefab, stateMachine.transform.position, Quaternion.identity);
            dead.GetComponentInChildren<SpriteRenderer>().flipX = stateMachine.Visual.GetComponent<SpriteRenderer>().flipX;
            stateMachine.DestroyThis();
        }
        else
        {
            stateMachine.DestroyThis();
        }
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}
