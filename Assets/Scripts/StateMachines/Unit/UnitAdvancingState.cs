using UnityEngine;

namespace StateMachines.Unit
{
    public class UnitAdvancingState : UnitBaseState
    {
        private const float CrossFadeDuration = 0.1f;
        private const float AnimatorDampTime = 0.1f;

        private readonly int RunningHash = Animator.StringToHash("Running");

        public UnitAdvancingState(UnitStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Animator.Play(RunningHash);
        }

        public override void Tick(float deltaTime)
        {
            Move(stateMachine.AdvancingDirection * stateMachine.MovementSpeed, deltaTime);
        
            if (stateMachine.Targeter.SelectClosestTarget(stateMachine.OtherFaction))
            {
                stateMachine.SwitchState(new UnitChasingState(stateMachine));
            }
        
            //stateMachine.Animator.SetFloat(SpeedHash, 0, 0.1f, deltaTime);
        }

        public override void Exit()
        {
        
        }
    }
}