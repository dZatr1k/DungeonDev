using ObjectPool;
using System;
using Units;
using UnityEngine;

public abstract class Bullet : PoolItem
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Weapon _weapon;
    private bool _isShooted = false;

    private Vector3 _direction;
    private Vector3 _targetPos;
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
            transform.Translate(_direction * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryDamageUnit(collision.gameObject.GetComponent<Unit>());
    }

    public void Shoot(Vector3 targetPos)
    {
        _targetPos = targetPos;
        _isShooted = true;

        _direction = (_targetPos - transform.position).normalized;
        _direction = transform.InverseTransformDirection(_direction);
    }

    protected abstract void TryDamageUnit<T>(T unit) where T : Unit;
}
