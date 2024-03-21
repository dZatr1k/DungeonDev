using System;
using UnityEngine;
using ObjectPool;
using Units;

public abstract class Weapon : PoolItem
{
    [SerializeField] private uint _damage;

    public uint Damage => _damage;
    public override Type ItemType { get => GetType(); }
    public override GameObject GameObject => gameObject;

    public abstract void Attack<T>(Unit attacking, Unit target) where T : Unit;
}
