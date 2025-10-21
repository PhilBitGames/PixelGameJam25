using System.Collections.Generic;
using Combat;
using StateMachines.Unit;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private List<UnitDefinition> unitDefinitions = new();

    private Dictionary<string, UnitDefinition> unitDefinitionsLookup;

    private void Awake()
    {
        unitDefinitionsLookup = new Dictionary<string, UnitDefinition>();
        foreach (var def in unitDefinitions)
            if (def != null && !string.IsNullOrEmpty(def.id))
                unitDefinitionsLookup[def.id] = def;
    }

    public GameObject CreateUnit(string id, Vector3 position, Faction faction)
    {
        if (!unitDefinitionsLookup.TryGetValue(id, out var def) || def == null)
        {
            Debug.LogWarning($"UnitFactory: missing definition or prefab for id '{id}'");
            return null;
        }

        var prefabToSpawn = faction == Faction.Player ? def.undeadPrefab : def.enemyPrefab;
        var go = Instantiate(prefabToSpawn, position, Quaternion.identity);
        var stateMachine = go.GetComponent<UnitStateMachine>();
        stateMachine?.Initialize(def);
        return go;
    }

    public List<UnitDefinition> GetUnitDefinitions()
    {
        return unitDefinitions;
    }

    public UnitDefinition GetUnitDefinitionById(string id)
    {
        unitDefinitionsLookup.TryGetValue(id, out var unitDefinition);
        return unitDefinition;
    }
}