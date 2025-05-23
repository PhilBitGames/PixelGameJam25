using System;
using Combat;
using StateMachines.Unit;
using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    public GameObject SelectionMarker;
    [SerializeField] public UnitType UnitType;

    public void SelectMe()
    {
        SelectionMarker.SetActive(true);
    }
    
    public void DeSelectMe()
    {
        SelectionMarker.SetActive(false);
    }
}
