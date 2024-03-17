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
        
        //Here I use Awake instead Start because I need initializate object earlier than Start (SetObzerveArea)
        private void Awake()
        {
            SetSettings();
        }

        private void OnEnable()
        {
            StartCoroutine(ReloadCoroutine());
        }

        private void OnTriggerEnter2D(Collider2D collision)
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

        public virtual void Attack(Unit enemy)
        {
            Debug.Log($"[{name}]: I'm attaking [{enemy.name}]!");
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

        protected override void ReleaseItem()
        {
            Health = _settings.Health;
            _isReloading = true;
            base.ReleaseItem();
        }
    }
}
