using System.Collections.Generic;
using System.Linq;
using Combat;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCreationManager : MonoBehaviour
{
    [SerializeField] private UnitFactory unitFactory;
    [SerializeField] private Transform UnitPanel;
    [SerializeField] private GameObject UnitPanelElementPrefab;

    private string currentUnitTypeSelected;

    private readonly Dictionary<string, UnitTypeSelectionProperties> unitTypeToUnitTypeSelectionProperties = new();

    private void Awake()
    {
        foreach (var unitDefinition in unitFactory.GetUnitDefinitions())
        {
            var unitPanelElementPrefab = Instantiate(UnitPanelElementPrefab, UnitPanel);
            unitPanelElementPrefab.GetComponent<UnitSelectionPanel>().Initialize(unitDefinition.icon, unitDefinition,
                () => SetCurrentTypeSelected(unitDefinition.id));

            var unitTypeSelectionProperties = new UnitTypeSelectionProperties(unitDefinition.startingSoulCount,
                unitPanelElementPrefab.GetComponent<UnitSelectionPanel>());
            unitTypeToUnitTypeSelectionProperties.Add(unitDefinition.id, unitTypeSelectionProperties);
        }

        currentUnitTypeSelected = unitTypeToUnitTypeSelectionProperties.First().Key;
        SetCurrentTypeSelected(currentUnitTypeSelected);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            var keys = unitTypeToUnitTypeSelectionProperties.Keys.ToList();
            var currentUnitIndex = keys.IndexOf(currentUnitTypeSelected);
            var nextIndex = (currentUnitIndex + 1) % keys.Count;
            SetCurrentTypeSelected(keys[nextIndex]);
        }
    }


    public void CreateUnit(Vector2 mousePosition)
    {
        var unitTypeProperties = unitTypeToUnitTypeSelectionProperties[currentUnitTypeSelected];

        if (unitTypeProperties.SoulsCount <= 0) return;

        unitFactory.CreateUnit(currentUnitTypeSelected, mousePosition, Faction.Player);
        unitTypeProperties.SoulsCount--;
        GetComponent<AudioSource>().Play();
    }

    public void AddSoul(string unitType)
    {
        unitTypeToUnitTypeSelectionProperties[unitType].SoulsCount++;
    }

    private void SetCurrentTypeSelected(string unitType)
    {
        currentUnitTypeSelected = unitType;
        foreach (var keyValuePair in unitTypeToUnitTypeSelectionProperties)
        {
            var type = keyValuePair.Key;
            var properties = keyValuePair.Value;
            properties.unitSelectionPanel.SetSelected(type == currentUnitTypeSelected);
        }
    }

    private class UnitTypeSelectionProperties
    {
        private int soulsCount;
        public readonly UnitSelectionPanel unitSelectionPanel;

        public UnitTypeSelectionProperties(int initialSouls, UnitSelectionPanel unitSelectionPanel)
        {
            this.unitSelectionPanel = unitSelectionPanel;
            SoulsCount = initialSouls;
        }

        public int SoulsCount
        {
            get => soulsCount;
            set
            {
                soulsCount = value;
                unitSelectionPanel.UpdateUnitCount(soulsCount);
            }
        }
    }
}