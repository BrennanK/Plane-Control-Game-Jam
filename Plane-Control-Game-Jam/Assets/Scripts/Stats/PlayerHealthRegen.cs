using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthRegen : MonoBehaviour
{
    [SerializeField] private float _hpPerSecond;
    [SerializeField] private float _fractionOfMaxHealthPerSecond;
    [SerializeField] private float _applyRegenInterval = 1; // 0 to apply continuously

    private float _regenAccumulator;
    private float _lastTime = float.NegativeInfinity;
    private OneStat _health;

    private void Awake()
    {
        _health = PlayerStats.Instance.GetStat("health");
    }

    private void Update()
    {
        _regenAccumulator += _hpPerSecond * Time.deltaTime;
        _regenAccumulator += _fractionOfMaxHealthPerSecond * _health.Max * Time.deltaTime;

        if (Time.time < _lastTime + _applyRegenInterval)
            return;
        _lastTime = Time.time;

        int regen = (int)_regenAccumulator;
        _regenAccumulator -= regen;
        if (regen > 0)
            _health.Change(regen);
    }
}
