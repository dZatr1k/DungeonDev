using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class NonRenewableAttack : IAttackBehaviour
{
    public void Attack(Unit attacking, Unit attacked, Weapon weapon)
    {
        weapon.Use(attacked);
    }
}
