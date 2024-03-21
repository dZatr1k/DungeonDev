using UnityEngine;
using Units.Enemies;

public class HeroBullet: Bullet
{
    protected override void TryDamageUnit<T>(T unit)
    {
        if (unit is Enemy enemy)
        {
            enemy.TakeDamage(_damage);
            ReleaseItem();
        }
    }
}
