using Units.Heroes;
using UnityEngine;

namespace Units.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    public class Enemy : Unit
    {
        protected override void Attack(Unit target)
        {
            TryAttack<Hero>(target);
        }
    }
}