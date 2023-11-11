using Units.Enemies;
using UnityEngine;

namespace Waves
{
    public class SubWave : MonoBehaviour
    {
        [SerializeField] private uint _subWaveDuration;
        [SerializeField] private Enemy[] _enemies;
        [SerializeField, Range(0.01f, 1f)] private float _spawnTime = 0.5f; //for how much of the total time all enemies will be spawned

        public uint SubWaveDuration => _subWaveDuration;

        public void StartSubWave(EnemiesSpawner enemySpawner)
        {
            float timeToSpawn = _subWaveDuration * _spawnTime;
            float spawnDelay = timeToSpawn / _enemies.Length;
            StartCoroutine(enemySpawner.SpawnEnemiesCoroutine(_enemies, spawnDelay));
        }
    }
}
