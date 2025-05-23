using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private LayerMask deadUnitLayerMask; // Layer for units with 2D colliders
    [SerializeField] private LayerMask spawnAreaLayerMask; // Layer for units with 2D colliders
    
    private SelectableUnit hoveredUnit;
    public EventHandler<SelectableUnit> OnUnitClicked;
    public EventHandler<Vector2> OnSpawnAreaClicked;

    private bool isHoveringOverSpawnArea;
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        HandleMouseHover(mousePosition);
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleMouseClick(mousePosition);
        }
    }

    private void HandleMouseHover(Vector2 mousePosition)
    {
        RaycastHit2D hitDeadUnitLayer = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, deadUnitLayerMask);

        if (hitDeadUnitLayer.collider != null)
        {
            SelectableUnit unit = hitDeadUnitLayer.collider.GetComponent<SelectableUnit>();
            if (unit != null)
            {
                if (hoveredUnit != unit)
                {
                    if (hoveredUnit != null)
                    {
                        hoveredUnit.DeSelectMe();
                    }

                    hoveredUnit = unit;
                    hoveredUnit.SelectMe();
                }
            }
            
            return;
        }
        else
        {
            if (hoveredUnit != null)
            {
                hoveredUnit.DeSelectMe();
                hoveredUnit = null;
            }
        }
        
        RaycastHit2D hitSpawnArea = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, spawnAreaLayerMask);

        if (hitSpawnArea.collider != null)
        {
            isHoveringOverSpawnArea = true;
            return;
        }
        else
        {
            isHoveringOverSpawnArea = false;
        }
    }
    
    private void HandleMouseClick(Vector2 mousePosition)
    {
        if(hoveredUnit != null)
        {
            OnUnitClicked?.Invoke(this, hoveredUnit);
            hoveredUnit.DeSelectMe();
            hoveredUnit.GetComponent<DeadUnit>().GetSoulTaken();
            hoveredUnit = null;
        }
        else if (isHoveringOverSpawnArea)
        {
            OnSpawnAreaClicked?.Invoke(this, mousePosition);
        }
    }
}
