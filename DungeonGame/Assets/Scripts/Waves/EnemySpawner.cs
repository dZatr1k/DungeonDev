using GameBoard;
using ObjectPool;
using System;
using Units.Enemies;
using UnityEngine;


namespace Waves
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PoolsCatalog _poolsCatalog;
        [SerializeField] private Cell[] _spawnCells;
        public Cell[] SpawnCells => _spawnCells;

        public event Action SpawnedEnemyDied;

        public void SpawnEnemyOnRandomCell(Enemy enemy, int index)
        {
            var obj = _poolsCatalog.GetPool(enemy).Get();
            obj.transform.position = _spawnCells[index % _spawnCells.Length].transform.position;
            obj.GetComponent<Enemy>().EnemyDied += EnemyDie;
        }

        private void EnemyDie(Enemy enemy)
        {
            enemy.EnemyDied -= EnemyDie;
            SpawnedEnemyDied?.Invoke();
        }
    }

}
