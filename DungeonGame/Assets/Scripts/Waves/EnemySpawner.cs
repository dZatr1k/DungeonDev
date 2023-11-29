using GameBoard;
using ObjectPool;
using System;
using Units;
using Units.Enemies;
using UnityEngine;


namespace Waves
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PoolsCatalog _poolsCatalog;
        [SerializeField] private Cell[] _spawnCells;
        public Cell[] SpawnCells => _spawnCells;

        public static event Action SpawnedEnemyDied;

        public void SpawnEnemyOnRandomCell(Enemy enemy, int index)
        {
            var obj = _poolsCatalog.GetPool(enemy).Get();
            obj.transform.position = _spawnCells[index % _spawnCells.Length].SpawnPoint.transform.position;
            obj.GetComponent<Enemy>().OnUnitDied += EnemyDie;
        }

        private void EnemyDie(Unit enemy)
        {
            enemy.OnUnitDied -= EnemyDie;
            Debug.Log("enemy Died");
            SpawnedEnemyDied?.Invoke();
        }
    }

}
