using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using Units.Enemies;

public class HeroThrowWeapon : Weapon
{
    [SerializeField] private float _pushSpeed;

    private void OnValidate()
    {
        _attackType = WeaponAttackType.Throw;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit enemy))
        {
            TryAttack<Enemy>(enemy);
        }
    }

    public override void Use(Unit to)
    {
        _body.velocity = Vector2.right * _pushSpeed;
    }
}
