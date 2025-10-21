using UnityEngine;

namespace StateMachines.Unit
{
    public class UnitAdvancingState : UnitBaseState
    {
        private const float TargetCheckInterval = 0.25f;
        private readonly int RunningHash = Animator.StringToHash("Running");

        private float targetCheckTimer;

        public UnitAdvancingState(UnitStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Animator.Play(RunningHash);
        }

        public override void Tick(float deltaTime)
        {
            Move(stateMachine.AdvancingDirection * stateMachine.MovementSpeed, deltaTime);

            targetCheckTimer -= deltaTime;
            if (targetCheckTimer > 0f) return;

            targetCheckTimer = TargetCheckInterval;
            if (stateMachine.Targeter.SelectClosestTarget(stateMachine.OtherFaction))
                stateMachine.SwitchState(new UnitChasingState(stateMachine));
        }

        public override void Exit()
        {
        }
    }
}