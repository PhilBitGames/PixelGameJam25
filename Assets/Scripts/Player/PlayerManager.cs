using System;
using Units;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        
        [SerializeField] RectTransform battleArea;
        [SerializeField] Ballista ballista;
        [SerializeField] SelectionManager selectionManager;
        [SerializeField] GameObject MoveTargetPrefab;
        [SerializeField] private Button NormalArrowButton;
        [SerializeField] private Button UndeadArrowButton;
        public enum CurrentState
        {
            Idle,
            UnitSelected,
            BallistaSelected,
        }

        private CurrentState currentState;

        private void Awake()
        {
            SetCurrentState(CurrentState.Idle);
            NormalArrowButton.onClick.AddListener(SelectArrow);
            UndeadArrowButton.onClick.AddListener(SelectArrow);
            selectionManager.OnSelectionClearedOrEmpty += OnSelectionClearedOrEmpty;
        }

        private void OnSelectionClearedOrEmpty()
        {
            if(currentState == CurrentState.UnitSelected)
            {
                SetCurrentState(CurrentState.Idle);
            }
        }


        private void Update()
        {
            if (!GameManager.Instance.IsPlaying())
            {
                return;
            }
            
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                SetCurrentState(CurrentState.Idle);
            }
            
            selectionManager.Tick();

            if (selectionManager.SelectedUnits.Count > 0)
            {
                currentState = CurrentState.UnitSelected;
            }
            

            Vector3 mousePos = Mouse.current.position.ReadValue();
            if (RectTransformUtility.RectangleContainsScreenPoint(battleArea, mousePos))
            {
                if (Camera.main != null)
                {


                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
                    worldPoint.z = 0;
                    mousePos = worldPoint;
                    switch (currentState)
                    {
                        case CurrentState.Idle:
                            
                            break;
                        case CurrentState.UnitSelected:
                            if (Mouse.current.rightButton.wasReleasedThisFrame)
                            {
                                MoveTarget moveTarget = Instantiate(MoveTargetPrefab, mousePos, Quaternion.identity).GetComponent<MoveTarget>();
                                moveTarget.SetExpectedUnitCount(selectionManager.SelectedUnits.Count);
                                foreach (var su in selectionManager.SelectedUnits)
                                {
                                    // su.unitStateMachine.MoveToTarget(moveTarget);
                                }
                            }
                            // Handle unit selected state
                            break;
                        case CurrentState.BallistaSelected:
                            ballista.Tick(Time.deltaTime, mousePos);
                            break;

                    }

                }
            }
        }
        
        public void SetCurrentState(CurrentState state)
        {
            currentState = state;
            switch (currentState)
            {
                case CurrentState.Idle:
                    ballista.SetArrowPathVisibility(false);
                    selectionManager.ClearSelectedUnits();
                    break;
                case CurrentState.UnitSelected:
                    ballista.SetArrowPathVisibility(false);
                    break;
                case CurrentState.BallistaSelected:
                    ballista.SetArrowPathVisibility(true);
                    selectionManager.ClearSelectedUnits();
                    break;
            }
        }
        
        
        private void SelectArrow()
        {
            SetCurrentState(CurrentState.BallistaSelected);
        }
    }
}