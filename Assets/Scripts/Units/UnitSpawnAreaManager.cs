using UnityEngine;

public class UnitSpawnAreaManager : MonoBehaviour
{
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private UnitCreationManager unitCreationManager;
    
    private void Start()
    {
        mouseManager.OnSpawnAreaClicked += HandleSpawnAreaClicked;
    }

    private void HandleSpawnAreaClicked(object sender, Vector2 mousePosition)
    {
        unitCreationManager.CreateUnit(mousePosition);
    }
}