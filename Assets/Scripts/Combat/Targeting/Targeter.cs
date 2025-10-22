using System.Collections.Generic;
using StateMachines.Unit;
using UnityEngine;

namespace Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        private readonly List<Target> potentialTargets = new();
        public Target CurrentTarget { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Target target))
            {
                potentialTargets.Add(target);
                target.OnDestroyed += RemoveTarget;
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Target target)) { return; }

            RemoveTarget(target);
        }

        public void InitializeCollider(float radius)
        {
            GetComponent<CircleCollider2D>().radius = radius;
        }

        
        public bool SelectClosestTarget(Faction faction)
        {
            if (potentialTargets.Count == 0) return false;

            Target closestTarget = null;
            var closestTargetDistance = Mathf.Infinity;

            foreach (var target in potentialTargets)
            {
                if (target.GetComponent<UnitStateMachine>().Faction != faction ||
                    target.GetComponent<Health>().IsDead ||
                    ReferenceEquals(target.gameObject, GetComponentInParent<Target>().gameObject))
                    continue;

                if (!target.GetComponentInChildren<Renderer>().isVisible) continue;

                var distanceSqrMag = (transform.position - target.transform.position).sqrMagnitude;

                if (distanceSqrMag < closestTargetDistance)
                {
                    closestTarget = target;
                    closestTargetDistance = distanceSqrMag;
                }
            }

            if (closestTarget == null) return false;

            CurrentTarget = closestTarget;

            return true;
        }

        private void RemoveTarget(Target target)
        {
            if (CurrentTarget == target) CurrentTarget = null;

            target.OnDestroyed -= RemoveTarget;
            potentialTargets.Remove(target);
        }
    }
}