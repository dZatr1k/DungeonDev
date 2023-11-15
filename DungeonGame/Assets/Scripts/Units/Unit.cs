using ObjectPool;
using System;
using UnityEngine;
using System.Collections;

namespace Units
{
    public abstract class Unit : PoolItem
    {
        [SerializeField] protected ContactFilter2D _contactFilter;
        [SerializeField] protected BoxCollider2D _observeArea;
        [SerializeField] protected int _health;
        [SerializeField] protected float _attackCooldown;
        [SerializeField] protected Weapon _weapon;
        [SerializeField] protected uint _cost;

        private IAttackBehaviour _attackBehaviour;
        private bool _isReloading = false;

        public Action OnUnitDie;
        public int Health => _health;
        public float AttackCooldown => _attackCooldown;

        public uint Cost => _cost;

        public override Type ItemType { get => GetType(); }
        public override GameObject GameObject => gameObject;

        private void Start()
        {
            if (_weapon == null)
                return;
            switch (_weapon.AttackType)
            {
                case WeaponAttackType.Throw:
                case WeaponAttackType.DoubleThrow:
                    _attackBehaviour = new RenewableAttack();
                    break;
                case WeaponAttackType.Hand:
                    _attackBehaviour = new NonRenewableAttack();
                    break;
            }
        }

        private void FixedUpdate()
        {
            CheckObserveArea();
        }

        private void CheckObserveArea()
        {
            if (_weapon == null || _isReloading)
                return;

            Collider2D[] hits = new Collider2D[3];
            _observeArea.OverlapCollider(_contactFilter, hits);

            foreach (var collision in hits)
            {
                if (collision == null)
                    continue;
                if (collision.gameObject.TryGetComponent(out Unit unit))
                {
                    Debug.Log("attack");
                    Attack(unit);
                    StartCoroutine(ReloadCoroutine());
                    break;
                }
            }
        }

        private IEnumerator ReloadCoroutine()
        {
            _isReloading = true;
            yield return new WaitForSeconds(RandomExtensions.GetNumberInEpsilonAmbit(_attackCooldown));
            _isReloading = false;
            Debug.Log("reload" + GetType());
        }

        public virtual void Die()
        {
            ReleaseItem();
            OnUnitDie?.Invoke();
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
    }
}
