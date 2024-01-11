using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour, IOnStatChange
{
    [SerializeField] private UIBarFill _barFill;

    private void Awake()
    {
        PlayerStats.Instance.GetStat("health").AddInform(this, true);
    }

    public void OnStatChange(OneStat stat, int newValue)
    {
        _barFill.SetFill(stat.FractionOfMax);
    }
}
