using StateMachines.Unit;
using UnityEngine;

namespace Units
{
    public class MoveTarget : MonoBehaviour
    {
        [SerializeField] private float upScaleFactor = 1.5f;
        private int expectedUnitCount;
        public void SetExpectedUnitCount(int selectedUnitsCount)
        {
            expectedUnitCount = selectedUnitsCount;
        }

        public void UnitArrived()
        {
            transform.localScale *= upScaleFactor;
        }
        
        public void DisregardUnit(UnitStateMachine unitStateMachine)
        {
            expectedUnitCount--;
            DestroyOnNoMoreUnits();
        }
        
        public void DestroyOnNoMoreUnits()
        {
            if (expectedUnitCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
