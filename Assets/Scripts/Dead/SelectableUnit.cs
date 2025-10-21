using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] private GameObject SelectionMarker;

    public string UnitType;

    public void SelectMe()
    {
        SelectionMarker.SetActive(true);
    }

    public void DeSelectMe()
    {
        SelectionMarker.SetActive(false);
    }
}