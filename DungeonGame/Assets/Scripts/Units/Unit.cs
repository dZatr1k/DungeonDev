using ObjectPool;
using System;
using UnityEngine;
using System.Collections;

namespace Units
{
    public abstract class Unit : PoolItem
    {
        private static int DefaultHealth;

        [SerializeField] protected BoxCollider2D _observeArea;
        [SerializeField] protected int _health;
        [SerializeField] protected float _abilityCooldown;
        [SerializeField] protected Weapon _weapon;
        [SerializeField] protected uint _cost;

        private IAttackBehaviour _attackBehaviour;
        private bool _isReloading = false;

        public int Health => _health;
        public CustomUnityPool WeaponPool { get; private set; }
        public uint Cost => _cost;
        public override Type ItemType { get => GetType(); }
        public override GameObject GameObject => gameObject;

        public event Action<Unit> OnUnitDied;
        
        private void Start()
        {
            DefaultHealth = _health;

            if (_weapon == null)
                return;
            switch (_weapon.AttackType)
            {
                case WeaponAttackType.Throw:
                case WeaponAttackType.DoubleThrow:
                    WeaponPool = PoolsCatalog.Instance.GetPool(_weapon);
                    _attackBehaviour = new RenewableAttack();
                    break;
                case WeaponAttackType.Hand:
                    _attackBehaviour = new NonRenewableAttack();
                    break;
            }
        }

        private void OnEnable()
        {
            StartCoroutine(ReloadCoroutine());
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_weapon == null || _isReloading)
                return;

            if (collision.gameObject.TryGetComponent(out Unit unit))
            {
                Attack(unit);
                StartCoroutine(ReloadCoroutine());
            }
        }

        private IEnumerator ReloadCoroutine()
        {
            _isReloading = true;
            yield return new WaitForSeconds(RandomExtensions.GetNumberInEpsilonAmbit(_abilityCooldown));
            _isReloading = false;
        }

        public virtual void Die()
        {
            ReleaseItem();
            OnUnitDied?.Invoke(this);
        }

        public virtual void Attack(Unit enemy)
        {
            _attackBehaviour?.Attack(this, enemy, _weapon);
        }

        public virtual bool IsAlive()
        {
            return _health > 0;
        }

        public virtual void TakeDamage(int damage)
        {
            _health = damage >= _health ? 0 : _health - damage;
            if (IsAlive() == false)
            {
                Die();
            }
        }

        protected override void ReleaseItem()
        {
            _health = DefaultHealth;
            _isReloading = false;
        }
    }
}
