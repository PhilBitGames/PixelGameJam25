using StateMachines.Unit;
using UnityEngine;

namespace Projectiles
{
    public class StandardArrow : BaseProjectile
    {
        [SerializeField] private int damage = 1;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.transform.CompareTag("Wall"))
            {
                Destroy(gameObject);
                return;
            }
            
            // Check if the arrow hit a target
            // if (other.transform.GetComponent<UnitStateMachine>().IsEnemy && !other.transform.GetComponent<Health>().IsDead)
            // {
            //
            //     // Deal damage to the enemy
            //     other.transform.GetComponent<Health>().DealDamage(damage);
            //     Destroy(gameObject); // Destroy the arrow after hitting the target
            // }

        }
    }
}