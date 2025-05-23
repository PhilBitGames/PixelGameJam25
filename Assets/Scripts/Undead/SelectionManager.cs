using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    
    public List<SelectableUnit> AllSelectableUnits = new List<SelectableUnit>();
    public List<SelectableUnit> SelectedUnits = new List<SelectableUnit>();
    private SelectableUnit PotentialClickableSelectableUnit = null;
    private bool isMouseDown;
    private bool isDragging;
    private Vector3 MouseStartPos;
    
    public Action OnSelectionClearedOrEmpty;

    private void OnMouseEnter()
    {
        throw new NotImplementedException();
    }

    private static SelectionManager instance;
    
    public static SelectionManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("SelectionManager is NULL");
            }
            return instance;
        }
    }
    
    public void AddSelectableUnit(SelectableUnit selectableUnit)
    {
        if (!AllSelectableUnits.Contains(selectableUnit))
        {
            AllSelectableUnits.Add(selectableUnit);
        }
    }
    
    public void RemoveSelectableUnit(SelectableUnit selectableUnit)
    {
        if (AllSelectableUnits.Contains(selectableUnit))
        {
            AllSelectableUnits.Remove(selectableUnit);
        }
        
        if(PotentialClickableSelectableUnit == selectableUnit)
        {
            PotentialClickableSelectableUnit = null;
        }
        
        if(SelectedUnits.Contains(selectableUnit))
        {
            SelectedUnits.Remove(selectableUnit);
        }
        
        if(SelectedUnits.Count == 0)
        {
            OnSelectionClearedOrEmpty?.Invoke();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void Tick()
    {
        //TODO: Keep working on this
        if (!isDragging)
        {
            SelectableUnit closestSelectableUnit = null;
            float closestDistance = Mathf.Infinity;
            
            foreach (SelectableUnit su in AllSelectableUnits)
            {
                Vector2 screenPos = Camera.main.WorldToScreenPoint(su.transform.position);
                var distance = (Mouse.current.position.ReadValue() - screenPos).sqrMagnitude;
                
                if(distance <= Math.Pow(25, 2))
                {
                    if (!SelectedUnits.Contains(su))
                    {
                        su.SelectMe();
                    }
                    
                    if (closestSelectableUnit == null)
                    {
                        closestSelectableUnit = su;
                        closestDistance = distance;
                    }
                    else
                    {           
                        if (distance < closestDistance)
                        {
                            if (!SelectedUnits.Contains(closestSelectableUnit))
                            {
                                closestSelectableUnit.DeSelectMe();
                            }
                            closestSelectableUnit = su;
                            closestDistance = distance;
                        }
                    }
                }
                else
                {
                    if (!SelectedUnits.Contains(su))
                    {
                        su.DeSelectMe();
                    }
                }
            }
            
            if(closestSelectableUnit != null)
            {
                PotentialClickableSelectableUnit = closestSelectableUnit;
            }
            // else
            // {
            //     if (PotentialClickableSelectableUnit != null)
            //     {
            //         PotentialClickableSelectableUnit.DeSelectMe();
            //         PotentialClickableSelectableUnit = null;
            //     }
            // }
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isMouseDown = true;
            MouseStartPos = Mouse.current.position.ReadValue();

            foreach (var su in SelectedUnits)
            {
                if(PotentialClickableSelectableUnit != null && su == PotentialClickableSelectableUnit)
                {
                    continue;
                }
                su.DeSelectMe();
            }
            SelectedUnits.Clear();
        }

        if (isMouseDown)
        {
            Debug.Log(Vector3.Distance(Mouse.current.position.ReadValue(), MouseStartPos));
            if (Vector3.Distance(Mouse.current.position.ReadValue(), MouseStartPos) > 1 && !isDragging)
            {
                isDragging = true;
                selectionBox.gameObject.SetActive(true);
            }

            if (isDragging)
            {
                float boxWidth = Mouse.current.position.ReadValue().x - MouseStartPos.x;
                float boxHeight = Mouse.current.position.ReadValue().y - MouseStartPos.y;
                
                selectionBox.sizeDelta= new Vector2(Mathf.Abs(boxWidth), Mathf.Abs(boxHeight));
                selectionBox.anchoredPosition= new Vector2(MouseStartPos.x + (boxWidth / 2), MouseStartPos.y + (boxHeight / 2));

                SelectUnits();
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (!isDragging && PotentialClickableSelectableUnit != null)
            {
                SelectedUnits.Add(PotentialClickableSelectableUnit);
            }
            
            isMouseDown = false;
            isDragging = false;
            selectionBox.gameObject.SetActive(false);
        }
    }

    private void SelectUnits()
    {
        foreach (SelectableUnit su in AllSelectableUnits)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(su.transform.position);
            float left = selectionBox.anchoredPosition.x - (selectionBox.sizeDelta.x / 2);
            float right = selectionBox.anchoredPosition.x + (selectionBox.sizeDelta.x / 2);
            float top = selectionBox.anchoredPosition.y + (selectionBox.sizeDelta.y / 2);
            float bottom = selectionBox.anchoredPosition.y - (selectionBox.sizeDelta.y / 2);
            
            if(screenPos.x > left && screenPos.x < right && screenPos.y < top && screenPos.y > bottom)
            {
                if (!SelectedUnits.Contains(su))
                {
                    SelectedUnits.Add(su);
                    su.SelectMe();
                }
            }
            else
            {
                if (SelectedUnits.Contains(su))
                {
                    SelectedUnits.Remove(su);
                    su.DeSelectMe();
                }
            }
        }
    }

    public void ClearSelectedUnits()
    {
        foreach (var su in SelectedUnits)
        {
            su.DeSelectMe();
        }
        PotentialClickableSelectableUnit?.DeSelectMe();
        SelectedUnits.Clear();
        OnSelectionClearedOrEmpty?.Invoke();
    }
}
