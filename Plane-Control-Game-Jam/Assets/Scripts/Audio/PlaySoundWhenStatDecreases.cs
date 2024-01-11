using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenStatDecreases : MonoBehaviour, IOnStatChange
{
    [SerializeField] private GameObject _soundPrefab;
    [SerializeField] private Transform _instantiatedSoundGameobjectParent;
    [SerializeField] private OneStat _stat;

    private int _priorStatValue = int.MinValue;

    private void Awake()
    {
        _stat.AddInform(this, true);
    }

    public void OnStatChange(OneStat stat, int newValue)
    {
        if (newValue < _priorStatValue)
            PlaySound();
        _priorStatValue = newValue;
    }

    private void PlaySound()
    {
        Instantiate(_soundPrefab, _instantiatedSoundGameobjectParent);
    }
}
