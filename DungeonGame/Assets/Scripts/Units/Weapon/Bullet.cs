using ObjectPool;
using System;
using UnityEngine;

public abstract class Bullet : PoolItem
{
    [SerializeField] private float _speed = 1f;
    private bool _isShooted = false;
    private Transform _target;

    public override Type ItemType { get => GetType(); }
    public override GameObject GameObject => gameObject;

    private void Update()
    {
        if (_isShooted)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDamageUnit(collision.gameObject);
    }

    public void Shoot(Transform target)
    {
        _target = target;
        _isShooted = true;
    }

    protected abstract void TryDamageUnit<T>(T unit);
}
