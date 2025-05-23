// using System.Collections;
// using System.Collections.Generic;
// using StateMachines.Unit;
// using UnityEngine;
//
// public class UndeadIdleState : UnitBaseState
// {
//     // Start is called before the first frame update
//     
//     private const float CrossFadeDuration = 0.1f;
//     private const float AnimatorDampTime = 0.1f;
//
//     private readonly int EntryHash = Animator.StringToHash("Entry");
//     
//     private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
//     private readonly int SpeedHash = Animator.StringToHash("Speed");
//
//     public UndeadIdleState(UnitStateMachine stateMachine) : base(stateMachine) { }
//
//     public override void Enter()
//     {
//         stateMachine.Animator.Play(EntryHash);
//     }
//
//     public override void Tick(float deltaTime)
//     {
//         Move(Vector3.right * stateMachine.MovementSpeed, deltaTime);
//         
//         if (stateMachine.Targeter.SelectClosestTarget(!stateMachine.IsEnemy))
//         {
//             stateMachine.SwitchState(new UnitChasingState(stateMachine));
//             return;
//         }
//
//         // Target closestAllyTarget = stateMachine.Targeter.SelectTargetThatClosestAllyIsTargeting(!stateMachine.IsEnemy);
//         //
//         // if(closestAllyTarget != null)
//         // {
//         //     stateMachine.Targeter.CurrentTarget = closestAllyTarget;
//         //     stateMachine.SwitchState(new UnitChasingState(stateMachine));
//         //     return;
//         // }
//     }
//
//     public override void Exit()
//     {
//         
//     }
//
// }
