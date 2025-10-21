using UnityEngine;

public class SoulTakingManager : MonoBehaviour
{
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private UnitCreationManager unitCreationManager;

    private void Start()
    {
        mouseManager.OnUnitClicked += HandleSoulTaken;
    }

    private void HandleSoulTaken(object sender, SelectableUnit selectableUnit)
    {
        unitCreationManager.AddSoul(selectableUnit.UnitType);
    }
}