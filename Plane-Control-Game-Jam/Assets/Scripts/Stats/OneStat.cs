using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// earlier in script execution order than PlayerStats

public class OneStat : MonoBehaviour
{
    [field: SerializeField] public string Title { get; private set; }
    [SerializeField] private int _initial = 1;
    [SerializeField] private int _min = 0;
    [SerializeField] private int _max = int.MaxValue;

    public int Value { get; private set; }

    private List<IOnStatChange> _toInform = new();

    public void AddInform(IOnStatChange add, bool informImmediately = false) 
    { 
        _toInform.Add(add);
        if (informImmediately)
            add.OnStatChange(this, Value);
    }
    public void RemoveInform(IOnStatChange remove) => _toInform.Remove(remove);

    private void Awake()
    {
        Value = _initial;
    }

    public void Change(int amount)
    {
        Value += amount;
        Value = System.Math.Min(_max, Value);
        Value = System.Math.Max(_min, Value);
        for (int i = 0; i < _toInform.Count; i++)
            _toInform[i].OnStatChange(this, Value);
    }

}
