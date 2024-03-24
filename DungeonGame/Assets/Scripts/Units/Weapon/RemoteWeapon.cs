using ObjectPool;
using Units;
using UnityEngine;

public class RemoteWeapon : Weapon
{
    [SerializeField] private Bullet _bullet;
    private CustomUnityPool _pool;
    protected Bullet Bullet => _bullet;

    private void Start()
    {
        _pool = PoolsCatalog.Instance.GetPool(_bullet);
    }

    public override void Attack<T>(Unit attacking, Unit target)
    {
        Bullet bullet = (Bullet)_pool.Get();
        bullet.transform.position = transform.position;
        bullet.Shoot(target.transform.position);
    }
}
