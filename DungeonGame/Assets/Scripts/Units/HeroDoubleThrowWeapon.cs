public class HeroDoubleThrowWeapon : HeroThrowWeapon
{
    private void OnValidate()
    {
        _attackType = WeaponAttackType.DoubleThrow;
    }
}
