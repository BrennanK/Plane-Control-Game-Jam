using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// earlier in script execution order

public class PlayerStats : MonoBehaviour
{
    private Dictionary<string, OneStat> _stats = new();

    public static PlayerStats Instance { get; private set; }

    private void Awake() 
    { 
        OneStat[] all = GetComponentsInChildren<OneStat>();
        foreach (OneStat x in all)
            _stats.Add(x.Title, x);
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public OneStat GetStat(string title)
    {
        return _stats[title];
    }
}
