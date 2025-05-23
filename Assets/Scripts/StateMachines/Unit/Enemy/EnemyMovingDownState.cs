// using System.Collections;
// using System.Collections.Generic;
// using StateMachines.Unit;
// using UnityEngine;
//
// public class EnemyMovingDownState : UnitBaseState
// {
//     // Start is called before the first frame update
//     
//     private const float CrossFadeDuration = 0.1f;
//     private const float AnimatorDampTime = 0.1f;
//
//     private readonly int RunningHash = Animator.StringToHash("Running");
//
//     public EnemyMovingDownState(UnitStateMachine stateMachine) : base(stateMachine) { }
//
//     public override void Enter()
//     {
//         stateMachine.Animator.Play(RunningHash);
//     }
//
//     public override void Tick(float deltaTime)
//     {
//         Move(Vector3.left * stateMachine.MovementSpeed, deltaTime);
//         
//         if (stateMachine.Targeter.SelectClosestTarget(!stateMachine.IsEnemy))
//         {
//             stateMachine.SwitchState(new UnitChasingState(stateMachine));
//         }
//         
//         //stateMachine.Animator.SetFloat(SpeedHash, 0, 0.1f, deltaTime);
//     }
//
//     public override void Exit()
//     {
//         
//     }
//
// }
