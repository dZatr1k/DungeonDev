using ObjectPool;
using System;
using UnityEngine;
using System.Collections;

namespace Units
{
    public abstract class Unit : PoolItem
    {
        [SerializeField] private UnitSettings _settings;
        [SerializeField] protected BoxCollider2D _observeArea;
        protected float _abilityCooldown;

        private bool _isReloading = false;

        public uint Health            { get; private set; }
        public Weapon Weapon          { get; private set; }
        public uint Cost              { get; private set; }

        public override Type ItemType { get => GetType(); }
        public override GameObject GameObject => gameObject;

        public event Action<Unit> OnUnitDied;

        private void Awake()
        {
            SetSettings();
            if (Weapon != null)
                Weapon = Instantiate(Weapon, transform);
        }

        private void OnEnable()
        {
            StartCoroutine(ReloadCoroutine());
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (Weapon == null || _isReloading || collision.tag == "Field")
                return;
            if (collision.gameObject.TryGetComponent(out Unit unit))
            {
                Attack(unit);
                StartCoroutine(ReloadCoroutine());
            }
        }

        private void SetSettings()
        {
            Health = _settings.Health;
            Weapon = _settings.Weapon;

            _abilityCooldown = _settings.AbilityCooldown;
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

        protected virtual void Attack(Unit unit)
        {
            TryAttack<Unit>(unit);
        }

        protected void TryAttack<T>(Unit target) where T : Unit
        {
            if (target is T prey)
                Weapon.Attack(this, prey);
        }

        public virtual bool IsAlive()
        {
            return Health > 0;
        }

        public virtual void TakeDamage(uint damage)
        {
            Health = damage >= Health ? 0 : Health - damage;
            if (IsAlive() == false)
            {
                Die();
            }
        }

        public override void SetDefaultSettings()
        {
            Health = _settings.Health;
            _isReloading = true;
        }
    }
}
