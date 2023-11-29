using System;
using System.Collections;
using Units.Enemies;
using UnityEngine;

namespace Waves
{
    public class SubWave : MonoBehaviour
    {
        [SerializeField] private uint _subWaveDuration;
        [SerializeField] private Enemy[] _enemies;
        [SerializeField, Range(0.01f, 1f)] private float _spawnTimePercentage = 0.5f; //for how much of the total time all enemies will be spawned
        private int _countOfLiveEnemies;
        private bool _isSubWaveEnded;

        public uint SubWaveDuration => _subWaveDuration;

        private void OnEnable()
        {
            EnemySpawner.SpawnedEnemyDied += MemberDie;
        }

        private void OnDisable()
        {
            EnemySpawner.SpawnedEnemyDied -= MemberDie;
        }


        public IEnumerator StartSubWaveCoroutine(EnemySpawner enemySpawner)
        {
            _countOfLiveEnemies = _enemies.Length;
            enemySpawner.SpawnCells.Shuffle();

            float spawnTime = (_subWaveDuration * _spawnTimePercentage);
            float spawnDelay = spawnTime / _enemies.Length;

            for (int i = 0; i < _enemies.Length; i++)
            {
                yield return new WaitForSeconds(spawnDelay);
                enemySpawner.SpawnEnemyOnRandomCell(_enemies[i], i);
            }

            float timer = 0f;
            float afterSpawnTime = _subWaveDuration - spawnTime;
            while (timer < afterSpawnTime)
            {
                timer += Time.deltaTime;
                if (_isSubWaveEnded)
                    yield break;
                yield return null;
            }

            EnemySpawner.SpawnedEnemyDied -= MemberDie;
        }

        private void MemberDie()
        {
            _countOfLiveEnemies--;
            _isSubWaveEnded = (_countOfLiveEnemies == 0);
        }
    }
}
