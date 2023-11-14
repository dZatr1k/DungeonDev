using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
public interface IAttackBehaviour
{
    void Attack(Unit attacking, Unit attacked, Weapon weapon);
}
