using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private LayerMask deadUnitLayerMask;
    [SerializeField] private LayerMask spawnAreaLayerMask;

    private SelectableUnit hoveredUnit;
    private bool isHoveringOverSpawnArea;
    public EventHandler<Vector2> OnSpawnAreaClicked;

    public EventHandler<SelectableUnit> OnUnitClicked;

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        HandleMouseHover(mousePosition);
        if (Mouse.current.leftButton.wasPressedThisFrame) HandleMouseClick(mousePosition);
    }

    private void HandleMouseHover(Vector2 mousePosition)
    {
        var hitDeadUnitLayer = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, deadUnitLayerMask);

        if (hitDeadUnitLayer.collider != null)
        {
            var unit = hitDeadUnitLayer.collider.GetComponent<SelectableUnit>();
            if (unit != null)
                if (hoveredUnit != unit)
                {
                    if (hoveredUnit != null) hoveredUnit.DeSelectMe();

                    hoveredUnit = unit;
                    hoveredUnit.SelectMe();
                }

            return;
        }

        if (hoveredUnit != null)
        {
            hoveredUnit.DeSelectMe();
            hoveredUnit = null;
        }

        var hitSpawnArea = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, spawnAreaLayerMask);

        isHoveringOverSpawnArea = hitSpawnArea.collider != null;
    }

    private void HandleMouseClick(Vector2 mousePosition)
    {
        if (hoveredUnit != null)
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