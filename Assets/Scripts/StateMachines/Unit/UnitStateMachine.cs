using Combat;
using Combat.Targeting;
using UnityEngine;

namespace StateMachines.Unit
{
    public class UnitStateMachine : StateMachine
    {
        [field: SerializeField] public Faction Faction { get; private set; }

        [field: SerializeField] public Animator Animator { get; private set; }

        [field: SerializeField] public WeaponDamage Weapon { get; private set; }

        [field: SerializeField] public Health Health { get; private set; }

        [field: SerializeField] public Targeter Targeter { get; private set; }

        [field: SerializeField] public GameObject Visual { get; private set; }

        public float MovementSpeed { get; private set; }

        public float ChasingRange { get; private set; }

        public float AttackRange { get; private set; }

        public int AttackDamage { get; private set; }

        public int AttackDamageVariance { get; private set; }

        public GameObject DeadPrefab { get; private set; }

        public UnitDefinition UnitDefinition { get; private set; }

        public Vector3 AdvancingDirection { get; private set; }

        public Faction OtherFaction => Faction == Faction.Enemy ? Faction.Player : Faction.Enemy;

        private void Start()
        {
            AdvancingDirection = Faction == Faction.Enemy ? Vector3.left : Vector3.right;
            SwitchState(new UnitAdvancingState(this));
        }

        private void OnEnable()
        {
            Health.OnDie += HandleDie;
        }

        private void OnDisable()
        {
            Health.OnDie -= HandleDie;
        }

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
                if (castle.Faction == Faction) return;

                castle.UnitAttackedCastle();
                DestroyThis();
            }
        }

        public void Initialize(UnitDefinition def)
        {
            UnitDefinition = def;
            MovementSpeed = def.movementSpeed;
            ChasingRange = def.chasingRange;
            AttackRange = def.attackRange;
            AttackDamage = def.attackDamage;
            AttackDamageVariance = def.attackDamageVariance;
            DeadPrefab = def.deadPrefab;
            Targeter.InitializeCollider();
        }

        private void HandleDie()
        {
            SwitchState(new UnitDeadState(this));
        }

        public void DestroyThis()
        {
            Destroy(gameObject);
        }
    }
}