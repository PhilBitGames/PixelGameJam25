using UnityEngine;

namespace StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }

        private void Update()
        {
            CurrentState?.Tick(Time.deltaTime);
        }

        public void SwitchState(State newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}