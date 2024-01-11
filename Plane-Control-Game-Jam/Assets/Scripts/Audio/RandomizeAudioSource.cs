using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSourceRandomClipOptions _clips;
    [SerializeField] private float _minPitch = .9f;
    [SerializeField] private float _maxPitch = 1.1f;
    [SerializeField] private float _minVolumeMult = .9f;
    [SerializeField] private float _maxVolumeMult = 1.1f;

    private static Dictionary<AudioSourceRandomClipOptions, RandomBag<AudioClip>> _bags = new();

    private void Awake()
    {
        if (!_bags.ContainsKey(_clips))
        {
            RandomBag<AudioClip> bag = new(_clips.Clips, _clips.Clips.Length / 2);
            _bags.Add(_clips, bag);
        }

        _audioSource.clip = _bags[_clips].Next();
        Debug.Log(_audioSource.clip == null);
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.volume *= Random.Range(_minVolumeMult, _maxVolumeMult);
        Debug.Log("volume: " + _audioSource.volume);

        _audioSource.gameObject.SetActive(true);
    }
}
