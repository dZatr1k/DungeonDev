using System;
using Units.Heroes;

namespace Units.Enemies
{
    public class Enemy : Unit<Hero>
    {
        public event Action<Enemy> EnemyDied;

        public void Disable()
        {
            EnemyDied?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}