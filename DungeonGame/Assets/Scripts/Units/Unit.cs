using ObjectPool;
using System;
using UnityEngine;

namespace Units
{
    public abstract class Unit<T> : PoolItem
    {
        [SerializeField] protected int _health;
        [SerializeField] protected float _attackCooldown;
        [SerializeField] protected int _damage;
        [SerializeField] protected int _cost;

        public int Health => _health;
        public float AttackCooldown => _attackCooldown;
        public int Damage => _damage;

        public int Cost => _cost;

        public override Type ItemType { get => GetType(); }
        public override GameObject GameObject => gameObject;

        public virtual void Attack(T enemy)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool IsAlive()
        {
            return _health > 0;
        }

        public virtual void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}
