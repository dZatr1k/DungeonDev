using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private string _volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(HendleSliderValueChanged);
    }

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter, 1);
        var volumeValue = Mathf.Lerp(-80f, 0f, _slider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
    }

    private void HendleSliderValueChanged(float value)
    {
        //var volumeValue = Mathf.Log10(value) * 20f;
        var volumeValue = Mathf.Lerp(-80f, 0f, value);
        _mixer.audioMixer.SetFloat(_volumeParameter, volumeValue);
    }
}
