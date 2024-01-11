using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SliderControlsAudioMixerGroupVolume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private string _volumeName = "Music Volume";

    public void OnSliderValueChange(float value)
    {
        _mixerGroup.audioMixer.SetFloat(_volumeName, 20 * Mathf.Log10(value));
    }
}
