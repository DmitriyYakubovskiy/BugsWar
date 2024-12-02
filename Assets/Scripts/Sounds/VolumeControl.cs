using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private string volueParameter = "Game";
    [SerializeField] private bool isMusic=false;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;

    private float volumeValue;
    private const float kValueVolume = 20f;

    private void Start()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);

        if (isMusic) volumeValue = SaveSystem.Data.musicVolume;
        else volumeValue = SaveSystem.Data.gameVolume;
        slider.value = Mathf.Pow(10f, volumeValue / kValueVolume);
        Debug.Log(volumeValue);
        Debug.Log(slider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        volumeValue = Mathf.Log10(value)*kValueVolume;
        mixer.SetFloat(volueParameter, volumeValue);
    }

    private void OnDisable()
    {
        if(isMusic) SaveSystem.Data.musicVolume = volumeValue;
        else SaveSystem.Data.gameVolume = volumeValue;
    }
}
