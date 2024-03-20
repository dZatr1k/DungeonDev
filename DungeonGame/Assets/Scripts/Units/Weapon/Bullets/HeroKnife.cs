using Units.Enemies;

public class HeroKnife : Bullet
{
    protected override void TryDamageUnit<T>(T unit)
    {
        if (unit is Enemy enemy)
        {
            enemy.TakeDamage(10);
        }
        ReleaseItem();
    }
}
