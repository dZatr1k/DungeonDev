using UnityEngine;
using Waves;

public class LevelProgressSystem : MonoBehaviour
{
    [SerializeField] private WavesSystem _wavesSystem;

    private void Start()
    {
        Debug.Log($"Waves in this Level: {_wavesSystem.Waves.Length}, total duration: { _wavesSystem.LevelDuration}");
        foreach (var wave in _wavesSystem.Waves)
        {
            Debug.Log($"{wave.name} -- duration: {wave.WaveDurationWithoutFinalAttack}, final Attack duration: {wave.FinalAttackDuration}");
        }
    }
}
