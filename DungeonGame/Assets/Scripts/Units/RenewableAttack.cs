using Units;
using ObjectPool;
using UnityEngine;

public class RenewableAttack : IAttackBehaviour
{

    public void Attack(Unit attacking, Unit attacked, Weapon weapon)
    {

        var createdWeapon = Object.FindObjectOfType<PoolsCatalog>().GetPool(weapon).Get();
        createdWeapon.transform.position = attacking.transform.position;
        createdWeapon.GetComponent<Weapon>().Use(attacked);
    }
}
