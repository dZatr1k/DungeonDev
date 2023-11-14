using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;

public class HandWeapon : Weapon
{
    private void OnValidate()
    {
        _attackType = WeaponAttackType.Hand;
    }

    public override void Use(Unit to)
    {
        to.TakeDamage(_damage);
    }
}
