using Units;
using Units.Heroes;
public class EnemyHand : MeleeWeapon
{
    public override void Attack<T>(Unit attacking, Unit target)
    {
        base.Attack<Hero>(attacking, target);
    }
}
