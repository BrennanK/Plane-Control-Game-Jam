using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceholderPlayerHealthUI : MonoBehaviour, IOnStatChange
{
    [SerializeField] private TMP_Text _text;

    private void Awake()
    {
        PlayerStats.Instance.GetStat("health").AddInform(this, true);
    }

    public void OnStatChange(OneStat stat, int newValue)
    {
        _text.text = "Player Health: " + newValue;
    }
}
