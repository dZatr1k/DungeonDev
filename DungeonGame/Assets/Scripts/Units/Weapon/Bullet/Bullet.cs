using ObjectPool;
using System;
using Units;
using UnityEngine;

public abstract class Bullet : PoolItem
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Weapon _weapon;
    private bool _isShooted = false;
    private Transform _target;
    protected uint _damage;

    public override Type ItemType { get => GetType(); }
    public override GameObject GameObject => gameObject;

    private void Start()
    {
        _damage = _weapon.Damage;
    }

    private void Update()
    {
        if (_isShooted)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryDamageUnit(collision.gameObject.GetComponent<Unit>());
    }

    public void Shoot(Transform target)
    {
        _target = target;
        _isShooted = true;
    }

    protected abstract void TryDamageUnit<T>(T unit) where T : Unit;
}
