using System.Collections;
using UnityEngine;

namespace Waves
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] private SubWave[] _subWaves;
        [SerializeField] private SubWave _finalSubWave;

        public uint WaveDurationWithoutFinalAttack
        {
            get
            {
                uint waveDuration = 0;
                foreach (var subWave in _subWaves)
                    waveDuration += subWave.SubWaveDuration;
                return waveDuration;
            }
        }
        public uint FinalAttackDuration => _finalSubWave.SubWaveDuration;
        public uint TotalWaveDuration => WaveDurationWithoutFinalAttack + FinalAttackDuration;

        public IEnumerator StartWaveCoroutine(EnemySpawner enemySpawner)
        {
            foreach (var subWave in _subWaves)
            {
                Debug.Log(subWave.name + " started");
                yield return StartCoroutine(subWave.StartSubWaveCoroutine(enemySpawner));
            }

            Debug.Log(_finalSubWave.name + " started");
            yield return StartCoroutine(_finalSubWave.StartSubWaveCoroutine(enemySpawner)); 
        }

    }
}
