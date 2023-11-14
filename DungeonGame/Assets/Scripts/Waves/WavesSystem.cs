using System.Collections;
using UnityEngine;
using LevelLogic;

namespace Waves
{
    public class WavesSystem : MonoBehaviour
    {
        [SerializeField] private Wave[] _waves;
        [SerializeField] private EnemySpawner _enemySpawner;

        public Wave[] Waves => _waves;

        public uint LevelDuration
        {
            get
            {
                uint levelDuration = 0;
                foreach (var waves in _waves)
                {
                    levelDuration += waves.TotalWaveDuration;
                }
                return levelDuration;
            }
        }

        private void OnEnable()
        {
            Level.Instance.CurrentStateMachine.OnGameStarted += StartSystem;
        }

        private void OnDisable()
        {
            Level.Instance.CurrentStateMachine.OnGameStarted -= StartSystem;
        }

        private void StartSystem()
        {
            StartCoroutine(StartWavesCoroutine());
        }

        private IEnumerator StartWavesCoroutine()
        {
            foreach (var wave in _waves)
            {
                Debug.Log($"{wave.name} started");
                yield return StartCoroutine(wave.StartWaveCoroutine(_enemySpawner));
            }
            Debug.Log("LEVEL END");
        }
    }
}
