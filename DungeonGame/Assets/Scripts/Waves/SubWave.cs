using System.Collections;
using Units.Enemies;
using UnityEngine;

namespace Waves
{
    public class SubWave : MonoBehaviour
    {
        [SerializeField] private uint _subWaveDuration;
        [SerializeField] private Enemy[] _enemies;

        public uint SubWaveDuration => _subWaveDuration;

        public IEnumerator StartSubWaveCoroutine()
        {
            yield return new WaitForSeconds(1);
        }
    }
}
