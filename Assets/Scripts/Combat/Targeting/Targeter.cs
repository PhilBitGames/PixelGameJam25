using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using StateMachines.Unit;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] UnitStateMachine stateMachine;
    public Target CurrentTarget { get; set; }

    private List<Target> targets = new List<Target>();

    private void Awake()
    {
        GetComponent<CircleCollider2D>().radius = stateMachine.ChasingRange;
    }

    public bool HasAnyTargets()
    {
        return targets.Count > 0;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Target target))
        {
            targets.Add(target);
            target.OnDestroyed += RemoveTarget;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
           
        RemoveTarget(target);
    }

    public bool SelectClosestTarget(Faction faction)
    {
        if (targets.Count == 0) { return false; }
        
        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;
        
        foreach (Target target in targets)
        {
            
            if(target.GetComponent<UnitStateMachine>().Faction != faction || 
               target.GetComponent<Health>().IsDead ||
               ReferenceEquals( target.gameObject, GetComponentInParent<Target>().gameObject))
            {
                continue;
            }
            
            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }
            
            var distanceSqrMag = (transform.position - target.transform.position).sqrMagnitude;
            
            if (distanceSqrMag < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = distanceSqrMag;
            }
        }
        
        if(closestTarget == null) { return false; }
        
        CurrentTarget = closestTarget;
        
        return true;
    }

    public void Cancel()
    {
        if(CurrentTarget ==  null) { return; }

        CurrentTarget = null;
    }
    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

    // public Target SelectTargetThatClosestAllyIsTargeting(bool lookingForEnemy)
    // {
    //     if (targets.Count == 0) { return null; }
    //     
    //     Target closestAllyTarget = null;
    //     float closestTargetDistance = Mathf.Infinity;
    //     
    //     foreach (Target target in targets)
    //     {
    //         
    //         if(target.GetComponent<UnitStateMachine>().IsEnemy == lookingForEnemy || 
    //            target.GetComponent<Health>().IsDead ||
    //            ReferenceEquals( target.gameObject, GetComponentInParent<Target>().gameObject))
    //         {
    //             continue;
    //         }
    //         
    //         if(target.GetComponent<UnitStateMachine>().Targeter.CurrentTarget == null)
    //         {
    //             continue;
    //         }
    //         
    //         if (!target.GetComponentInChildren<Renderer>().isVisible)
    //         {
    //             continue;
    //         }
    //         
    //         var distanceSqrMag = (transform.position - target.transform.position).sqrMagnitude;
    //         
    //         if (distanceSqrMag >= closestTargetDistance)
    //         {
    //             continue;   
    //         }
    //         
    //         closestAllyTarget = target.GetComponent<UnitStateMachine>().Targeter.CurrentTarget;
    //         closestTargetDistance = distanceSqrMag;
    //     }
    //     
    //     return closestAllyTarget;
    // }
}
