using System;
using Combat;
using Units;
using UnityEngine;

namespace StateMachines.Unit
{
    public class UnitStateMachine : StateMachine
    {

        
        [field: SerializeField]
        public Faction Faction{ get; private set; }
        
        [field: SerializeField]
        public UnitType UnitType{ get; private set; }
        
        [field: SerializeField]
        public Animator Animator { get; private set; }
    
        [field: SerializeField]
        public WeaponDamage Weapon { get; private set; } 
    
        [field: SerializeField]
        public Health Health { get; private set; }
    
        [field: SerializeField]
        public Target Target { get; private set; }
        
        [field: SerializeField]
        public Targeter Targeter { get; private set; }

        [field: SerializeField]
        public float MovementSpeed { get; private set; }
        
        [field: SerializeField]
        public float ChasingRange { get; private set; }
    
        [field: SerializeField]
        public float AttackRange { get; private set; }
    
        [field: SerializeField]
        public int AttackDamage { get; private set; }
        
        [field: SerializeField]
        public int AttackDamageVariance { get; private set; }
        
        [field: SerializeField]
        public GameObject UndeadPrefab { get; private set; }
        
        [field: SerializeField]
        public GameObject DeadPrefab { get; private set; }

        
        [field: SerializeField]
        public GameObject Visual { get; private set; }
        
        public Vector3 AdvancingDirection { get; private set; }
        
        // [field: SerializeField]
        // public int AttackKnockback { get; private set; }
        
        // public Health Player { get; private set; }
        
        public Faction OtherFaction => Faction == Faction.Enemy ? Faction.Player : Faction.Enemy;

        private void Start()
        {
            // Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            
            // Agent.updatePosition = false;
            // Agent.updateRotation = false;
            
            AdvancingDirection = Faction == Faction.Enemy ? Vector3.left : Vector3.right;
            SwitchState(new UnitAdvancingState(this));
            
            // if(TryGetComponent(out SelectableUnit selectableUnit))
            // {
            //     SelectionManager.Instance.AddSelectableUnit(selectableUnit);
            // }
        }
    
        private void OnEnable()
        {
            Health.OnTakeDamage += HandleTakeDamage;
            Health.OnDie += HandleDie;
        }
    
        private void OnDisable()
        {
            Health.OnTakeDamage -= HandleTakeDamage;
            Health.OnDie -= HandleTakeDamage;
        }
    
        private void HandleTakeDamage()
        {
            //SwitchState(new EnemyImpactState(this));
        }
        
        private void HandleDie()
        {
            SwitchState(new UnitDeadState(this));
        }
    
        // private void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        // }
        // public void TurnIntoUndead()
        // {
        //     Instantiate(UndeadPrefab, 
        //         transform.position, 
        //         Quaternion.identity, 
        //         GameObject.Find("UndeadContainer").transform);
        //     Destroy(gameObject);
        // }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ChasingRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out Castle castle))
            {
                if (castle.Faction == Faction)
                {
                    return;
                }
                
                castle.UnitAttackedCastle();
                //Just disappear for now
                DestroyThis();
                
            }

            // if(CurrentState is UndeadMovingState state)
            // {
            //     if (other.CompareTag("MoveTarget"))
            //     {
            //         MoveTarget moveTarget = other.GetComponent<MoveTarget>();
            //         if (moveTarget != null && state.moveTarget == moveTarget)
            //         {
            //             moveTarget.UnitArrived();
            //             SwitchState(new UndeadIdleState(this));
            //         }
            //     }
            // }
        }

        public void MoveToTarget(MoveTarget moveTarget)
        {
            SwitchState(new UndeadMovingState(this, moveTarget));
        }

        public void DestroyThis()
        {
            Destroy(gameObject);
        }
    }
}