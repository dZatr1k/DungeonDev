using System;
using UnityEngine;
using Units.Heroes;

namespace Units.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    public class Enemy : Unit
    {
        public event Action<Enemy> EnemyDied;

        public void Disable()
        {
            EnemyDied?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}