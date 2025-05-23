using StateMachines.Unit;
using UnityEngine;

namespace Projectiles
{
    public class UndeadArrow : BaseProjectile
    { 
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.transform.CompareTag("Wall"))
            {
                Destroy(gameObject);
                return;
            }
            
            // Check if the arrow hit a target
            if (other.transform.CompareTag("Enemy") && other.transform.GetComponent<Health>().IsDead)
            {
                // Deal damage to the enemy
                // other.transform.GetComponent<UnitStateMachine>().TurnIntoUndead();
                Destroy(gameObject); // Destroy the arrow after hitting the target
            }
            // else if (collision.CompareTag("Obstacle"))
            // {
            //     Destroy(gameObject); // Destroy the arrow on obstacle collision
            // }
        }
    }
}