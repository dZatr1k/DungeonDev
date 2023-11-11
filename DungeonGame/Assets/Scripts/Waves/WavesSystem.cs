using System.Collections;
using UnityEngine;

namespace Waves
{
    public class WavesSystem : MonoBehaviour
    {
        [SerializeField] private Wave[] _waves;
        [SerializeField] private EnemiesSpawner _enemySpawner;

        private void Start()
        {
            StartCoroutine(StartWavesCoroutine());
        }

        private IEnumerator StartWavesCoroutine()
        {
            foreach (var wave in _waves)
            {
                Debug.Log(wave.name + " Started");
                StartCoroutine(wave.StartWaveCoroutine(_enemySpawner));
                yield return new WaitForSeconds(wave.WaveDurationWithoutFinalAttack + wave.FinalAttackDuration);
            }
        }
    }
}
