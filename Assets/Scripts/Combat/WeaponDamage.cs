using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private int damage;
    private Health targetHealth;

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.TryGetComponent<Health>(out Health health))
    //     {
    //         health.DealDamage(damage);
    //     }
    // }

    public void DealDamageToTarget()
    {
        if (!targetHealth.IsDead)
        {
            targetHealth.DealDamage(damage);
        }
    }
    
    public void SetAttack(int damage, Target target)
    {
        this.targetHealth = target.GetComponent<Health>();
        this.damage = damage;
    }
}
