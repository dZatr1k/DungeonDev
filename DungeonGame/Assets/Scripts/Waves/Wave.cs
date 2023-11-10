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

        public IEnumerator StartWaveCoroutine()
        {
            foreach (var subWave in _subWaves)
            {
                StartCoroutine(subWave.StartSubWaveCoroutine());
                yield return new WaitForSeconds(subWave.SubWaveDuration);
            }

            StartFinalSubWave.Invoke();
            StartCoroutine(_finalSubWave.StartSubWaveCoroutine());
            yield return new WaitForSeconds(_finalSubWave.SubWaveDuration);
        }
    }
}
