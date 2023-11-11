using GameBoard;
using ObjectPool;
using System.Collections;
using Units.Enemies;
using UnityEngine;

namespace Waves
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private PoolsCatalog _poolsCatalog;
        [SerializeField] private Cell[] _spawnCells;

        public IEnumerator SpawnEnemiesCoroutine(Enemy[] enemies, float spawnDelay)
        {
            _spawnCells.Shuffle();

            for (int i = 0; i < enemies.Length; i++)
            {
                var enemy = _poolsCatalog.GetPool(enemies[i]).Get();
                enemy.transform.position = _spawnCells[i % enemies.Length].transform.position;
                yield return new WaitForSeconds(spawnDelay);

            }
        }
    }

}
