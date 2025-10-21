using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private int damage;
    private Health targetHealth;

    public void DealDamageToTarget()
    {
        if (!targetHealth.IsDead) targetHealth.DealDamage(damage);
    }

    public void SetAttack(int damage, Target target)
    {
        targetHealth = target.GetComponent<Health>();
        this.damage = damage;
    }
}