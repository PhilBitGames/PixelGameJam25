using System;
using Combat;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] public Faction Faction;
    public EventHandler OnCastleAttacked;

    public void UnitAttackedCastle()
    {
        OnCastleAttacked?.Invoke(this, EventArgs.Empty);
    }
}