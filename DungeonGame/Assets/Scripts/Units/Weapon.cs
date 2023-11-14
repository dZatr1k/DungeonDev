using System;
using UnityEngine;
using ObjectPool;
using Units;

public abstract class Weapon : PoolItem
{
    [SerializeField] protected int _damage;
    [SerializeField] protected Rigidbody2D _body;
    [SerializeField] protected WeaponAttackType _attackType;

    public WeaponAttackType AttackType => _attackType;
    public override Type ItemType { get => GetType(); }
    public override GameObject GameObject => gameObject;

    protected void TryAttack<T>(Unit unit)
    {
        if(unit is T)
        {
            unit.TakeDamage(_damage);
            ReleaseItem();
        }
    }

    public abstract void Use(Unit to);
}
