using UnityEngine;

public class UnitAnimationEventManager : MonoBehaviour
{
    [SerializeField] private WeaponDamage weaponDamage;

    public void TriggerDamage()
    {
        weaponDamage.DealDamageToTarget();
    }
}