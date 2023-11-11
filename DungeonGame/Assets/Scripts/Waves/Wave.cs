using System;
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
                for (int i = 0; i < _subWaves.Length - 1; i++)
                {
                    waveDuration += _subWaves[i].SubWaveDuration;
                }
                return waveDuration;
            }
        }
        public uint FinalAttackDuration => _finalSubWave.SubWaveDuration;

        public static event Action StartFinalSubWave;

        public IEnumerator StartWaveCoroutine(EnemiesSpawner enemySpawner)
        {
            foreach (var subWave in _subWaves)
            {
                Debug.Log(subWave.name + " started");
                subWave.StartSubWave(enemySpawner);
                yield return new WaitForSeconds(subWave.SubWaveDuration);
            }

            StartFinalSubWave?.Invoke();
            Debug.Log(_finalSubWave.name + " started");
            _finalSubWave.StartSubWave(enemySpawner);
            yield return new WaitForSeconds(_finalSubWave.SubWaveDuration);
        }
    }
}
