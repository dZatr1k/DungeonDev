using System.Collections;
using UnityEngine;

namespace Waves
{
    public class WavesSystem : MonoBehaviour
    {
        [SerializeField] private Wave[] _waves;

        private void Start()
        {
            StartCoroutine(StartWavesCoroutine());
        }

        private IEnumerator StartWavesCoroutine()
        {
            foreach (var wave in _waves)
            {
                StartCoroutine(wave.StartWaveCoroutine());
                yield return new WaitForSeconds(wave.WaveDurationWithoutFinalAttack + wave.FinalAttackDuration);
            }
        }
    }
}
