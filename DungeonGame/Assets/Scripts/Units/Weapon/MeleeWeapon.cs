using Units;

public abstract class MeleeWeapon : Weapon
{
    public override void Attack<T>(Unit attacking, Unit target)
    {
        if (target is T unit)
        {
            unit.TakeDamage(Damage);
        }
    }
}
