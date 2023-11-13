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
        [SerializeField, Range(0.01f, 1f)] private float _spawnTime = 0.5f; //for how much of the total time all enemies will be spawned
        private int _countOfLiveEnemies;
        private bool _isSubWaveEnded;

        public uint SubWaveDuration => _subWaveDuration;

        public IEnumerator StartSubWaveCoroutine(EnemySpawner enemySpawner)
        {
            enemySpawner.SpawnedEnemyDied += MemberDie;

            _countOfLiveEnemies = _enemies.Length;
            enemySpawner.SpawnCells.Shuffle();

            float spawnTime = (_subWaveDuration * _spawnTime);
            float afterSpawnTime = _subWaveDuration - spawnTime;
            float spawnDelay = spawnTime / _enemies.Length;

            for (int i = 0; i < _enemies.Length; i++)
            {
                yield return new WaitForSeconds(spawnDelay);
                enemySpawner.SpawnEnemyOnRandomCell(_enemies[i], i);
            }

            float counter = 0;
            while(counter < afterSpawnTime)
            {
                counter += Time.deltaTime;
                if (_isSubWaveEnded)
                    yield break;
                yield return null;
            }

            enemySpawner.SpawnedEnemyDied -= MemberDie;
        }

        private void MemberDie()
        {
            _countOfLiveEnemies--;
            _isSubWaveEnded = (_countOfLiveEnemies == 0);
        }
    }
}
