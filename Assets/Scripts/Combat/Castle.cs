using System;
using Combat;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public EventHandler OnCastleAttacked;
    [SerializeField] public Faction Faction;
    
    public void UnitAttackedCastle()
    {
        OnCastleAttacked?.Invoke(this, EventArgs.Empty);
    }
}
