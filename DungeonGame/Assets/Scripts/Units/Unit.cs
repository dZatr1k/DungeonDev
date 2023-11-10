using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public abstract class Unit<TOpponent> : MonoBehaviour
    {
        [SerializeField] protected int _health;
        [SerializeField] protected float _attackCooldown;
        [SerializeField] protected int _damage;
        [SerializeField] protected uint _cost;

        public int Health => _health;
        public float AttackCooldown => _attackCooldown;
        public int Damage => _damage;

        public uint Cost => _cost;

        public virtual void Attack(TOpponent enemy)
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
